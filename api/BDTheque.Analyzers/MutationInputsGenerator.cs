namespace BDTheque.Analyzers;

using BDTheque.Analyzers.Extensions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;

[Generator]
public class MutationInputsGenerator : IIncrementalGenerator
{
    private const string CreateInputTypeSuffix = "CreateInput";
    private const string UpdateInputTypeSuffix = "UpdateInput";
    private const string NestedTypeSuffix = "NestedInput";
    private const string InputsNamespaceName = "BDTheque.Model.Inputs";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<GeneratorSyntaxContext> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (s, _) => s is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                static (ctx, _) => ctx
            )
            .Where(static ctx => ((ClassDeclarationSyntax) ctx.Node).IsAnnotatedWithObjectType(ctx));

        context.RegisterSourceOutput(
            classDeclarations,
            static (sourceContext, generatorContext) => GenerateInputTypes(sourceContext, generatorContext)
        );
    }

    private static void GenerateInputTypes(SourceProductionContext sourceContext, GeneratorSyntaxContext generatorContext)
    {
        var classDeclaration = (ClassDeclarationSyntax) generatorContext.Node;
        CompilationUnitSyntax? compilationUnitSyntax = GenerateClassSource(classDeclaration, generatorContext);
        if (compilationUnitSyntax is null) return;

        string sourceText = compilationUnitSyntax.NormalizeWhitespace().ToFullString();
        sourceContext.AddSource($"{classDeclaration.Identifier.Text}Inputs.g.cs", sourceText);
    }

    private static CompilationUnitSyntax? GenerateClassSource(ClassDeclarationSyntax classDeclaration, GeneratorSyntaxContext context)
    {
        if (classDeclaration.SemanticModel(context).GetDeclaredSymbol(classDeclaration) is not { } classSymbol) return null;
        if ((classSymbol.BaseType is not { } baseTypeSymbol) || (baseTypeSymbol.SpecialType == SpecialType.System_Object)) return null;

        const string entitySuffix = "Entity";
        var baseClassName = baseTypeSymbol.ToDisplayParts().Last().ToString();
        if (!baseClassName.EndsWith(entitySuffix))
            return null;
        baseClassName = baseClassName.Substring(0, baseClassName.Length - entitySuffix.Length);

        ClassDeclarationSyntax[] inputClassesDeclaration = GenerateInputClassesDeclaration(context, classDeclaration, baseClassName);
        ClassDeclarationSyntax nestedClassDeclaration = GenerateNestedClassDeclaration(context, classDeclaration, baseClassName);

        return SyntaxFactory
            .CompilationUnit()
            .AddUsings(classDeclaration.SyntaxTree.GetCompilationUnitRoot().Members.OfType<BaseNamespaceDeclarationSyntax>().SelectMany(syntax => syntax.Usings).ToArray())
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(classSymbol.ContainingNamespace.ToDisplayString())),
                SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("HotChocolate.Types"))
            )
            .AddMembers(
                SyntaxFactory
                    .NamespaceDeclaration(SyntaxFactory.IdentifierName(InputsNamespaceName)) // the generated source is more readable than with a FileScopedNamespaceDeclaration
                    .AddMembers(
                        inputClassesDeclaration.OfType<MemberDeclarationSyntax>().ToArray()
                    )
                    .AddMembers(
                        nestedClassDeclaration
                    )
            )
            .NormalizeWhitespace()
            .WithLeadingTrivia(
                SyntaxFactory.Comment("// <auto-generated/>"),
                SyntaxFactory.Trivia(SyntaxFactory.NullableDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.EnableKeyword), true))
            );
    }

    private static ClassDeclarationSyntax GenerateInputClassDeclaration(GeneratorSyntaxContext context, ClassDeclarationSyntax classDeclaration, string baseClassName, string classSuffix) =>
        SyntaxFactory
            .ClassDeclaration(classDeclaration.Identifier.Text + classSuffix)
            .WithModifiers(classDeclaration.Modifiers)
            .AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(baseClassName + classSuffix))
            )
            .AddGeneratedAttribute<MutationInputsGenerator>()
            .AddMembers(
                classDeclaration.Members
                    .OfType<PropertyDeclarationSyntax>()
                    .Where(
                        property => !property.Type.IsCollectionType(context) &&
                                    !property.IsEntityIdProperty(context) &&
                                    !property.Identifier.Text.EndsWith("Raw")
                    )
                    .Select(
                        property =>
                        {
                            TypeSyntax? scalarType = property.GetMutationType(context);

                            return property // duplicate original property with some alterations
                                .WithType(
                                    SyntaxFactory
                                        .GenericName(SyntaxFactory.Identifier("global::HotChocolate.Optional"))
                                        .AddTypeArgumentListArguments(
                                            property.Type.RewriteType(context, syntax => SyntaxFactory.IdentifierName(scalarType == null ? syntax + NestedTypeSuffix : "ushort"))
                                        )
                                )
                                .ApplyMutationType(context, property.Type, scalarType)
                                .AddRequiredAttribute(classSuffix == CreateInputTypeSuffix && property.Type is not NullableTypeSyntax)
                                .WithInitializer(null).WithSemicolonToken(default);
                        }
                    )
                    .Cast<MemberDeclarationSyntax>()
                    .ToArray()
            );

    private static ClassDeclarationSyntax[] GenerateInputClassesDeclaration(GeneratorSyntaxContext context, ClassDeclarationSyntax classDeclaration, string baseClassName) =>
    [
        GenerateInputClassDeclaration(context, classDeclaration, baseClassName, CreateInputTypeSuffix),
        GenerateInputClassDeclaration(context, classDeclaration, baseClassName, UpdateInputTypeSuffix)
    ];

    private static ClassDeclarationSyntax GenerateNestedClassDeclaration(GeneratorSyntaxContext context, ClassDeclarationSyntax classDeclaration, string baseClassName) =>
        SyntaxFactory
            .ClassDeclaration(classDeclaration.Identifier.Text + NestedTypeSuffix)
            .WithModifiers(classDeclaration.Modifiers)
            .AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(baseClassName + NestedTypeSuffix))
            )
            .AddGeneratedAttribute<MutationInputsGenerator>()
            .AddMembers(
                classDeclaration.Members
                    .OfType<PropertyDeclarationSyntax>()
                    .Where(propertySyntax => propertySyntax.IsAnnotatedWithIdType(context))
                    .Select(property => property.AddRequiredAttribute())
                    .Cast<MemberDeclarationSyntax>()
                    .ToArray()
            )
            .AddImplicitConverter(
                classDeclaration
                    .AllMembers(context)
                    .OfType<PropertyDeclarationSyntax>()
                    .OnlyOrDefault(syntax => syntax.IsAnnotatedWithIdType(context))
            );
}
