namespace BDTheque.Analyzers.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;

[Generator]
public class MutationInputsGenerator : IIncrementalGenerator
{
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
        if (GenerateCompilationUnit(classDeclaration, generatorContext) is not { } compilationUnitSyntax)
            return;

        string sourceText = compilationUnitSyntax.NormalizeWhitespace().ToFullString();
        sourceContext.AddSource($"{classDeclaration.Identifier.Text}Inputs.g.cs", sourceText);
    }

    private static CompilationUnitSyntax? GenerateCompilationUnit(ClassDeclarationSyntax classDeclaration, GeneratorSyntaxContext context)
    {
        if (classDeclaration.SemanticModel(context).GetDeclaredSymbol(classDeclaration) is not { } classSymbol) return null;
        if ((classSymbol.BaseType is not { } baseTypeSymbol) || (baseTypeSymbol.SpecialType == SpecialType.System_Object)) return null;

        const string entitySuffix = "Entity";
        var baseClassName = baseTypeSymbol.ToDisplayParts().Last().ToString();
        if (!baseClassName.EndsWith(entitySuffix))
            return null;
        baseClassName = baseClassName[..^entitySuffix.Length];

        var members = new List<MemberDeclarationSyntax>
        {
            GenerateNestedClassDeclaration(context, classDeclaration, baseClassName)
        };
        if (!classDeclaration.IsAnnotatedWithStaticEntityAttribute(context))
        {
            InterfaceDeclarationSyntax inputInterfaceDeclaration = GenerateInputInterfaceDeclaration(context, classDeclaration);
            members.AddRange(
                [
                    inputInterfaceDeclaration,
                    ..GenerateInputClassesDeclaration(context, classDeclaration, baseClassName, inputInterfaceDeclaration)
                ]
            );
        }

        return SyntaxFactory
            .CompilationUnit()
            .AddUsings(classDeclaration.SyntaxTree.GetCompilationUnitRoot().Members.OfType<BaseNamespaceDeclarationSyntax>().SelectMany(syntax => syntax.Usings).ToArray())
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(classSymbol.ContainingNamespace.ToDisplayString())),
                SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(WellKnownDefinitions.HotChocolate.Types.Namespace))
            )
            .AddMembers(
                SyntaxFactory
                    .NamespaceDeclaration(SyntaxFactory.IdentifierName(WellKnownDefinitions.InputsNamespaceName)) // the generated source is more readable than with a FileScopedNamespaceDeclaration
                    .AddMembers(members.ToArray())
            )
            .NormalizeWhitespace()
            .SetAutoGenerated()
            .ActivateNullability();
    }

    private static InterfaceDeclarationSyntax GenerateInputInterfaceDeclaration(GeneratorSyntaxContext context, ClassDeclarationSyntax classDeclaration) =>
        SyntaxFactory
            .InterfaceDeclaration("I" + classDeclaration.Identifier + "InputType")
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddGeneratedAttribute<MutationInputsGenerator>()
            .AddMembers(
                classDeclaration.MutableProperties(context)
                    .Select(
                        property =>
                        {
                            TypeSyntax? scalarType = property.GetMutationType(context);

                            return SyntaxFactory
                                .PropertyDeclaration(property.Type, property.Identifier)
                                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                .WithType(
                                    SyntaxFactory
                                        .GenericName(SyntaxFactory.Identifier($"global::{WellKnownDefinitions.HotChocolate.OptionalType}"))
                                        .AddTypeArgumentListArguments(
                                            property.Type.RewriteType(context, syntax => SyntaxFactory.IdentifierName(scalarType == null ? syntax + WellKnownDefinitions.NestedTypeSuffix : "ushort"))
                                        )
                                )
                                .AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                                .WithSemicolonToken(default);
                        }
                    )
                    .Cast<MemberDeclarationSyntax>()
                    .ToArray()
            )
            .AddApplyToMethod(context, classDeclaration);


    private static ClassDeclarationSyntax GenerateInputClassDeclaration(GeneratorSyntaxContext context, ClassDeclarationSyntax classDeclaration, string baseClassName, InterfaceDeclarationSyntax inputInterfaceDeclaration, string classSuffix) =>
        SyntaxFactory
            .ClassDeclaration(classDeclaration.Identifier + classSuffix)
            .WithModifiers(classDeclaration.Modifiers)
            .AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(baseClassName + classSuffix)),
                SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(inputInterfaceDeclaration.Identifier))
            )
            .AddGeneratedAttribute<MutationInputsGenerator>()
            .AddMembers(
                classDeclaration.MutableProperties(context)
                    .Select(
                        property =>
                        {
                            TypeSyntax? scalarType = property.GetMutationType(context);

                            return property // duplicate original property with some alterations
                                .WithType(
                                    SyntaxFactory
                                        .GenericName(SyntaxFactory.Identifier($"global::{WellKnownDefinitions.HotChocolate.OptionalType}"))
                                        .AddTypeArgumentListArguments(
                                            property.Type.RewriteType(context, syntax => SyntaxFactory.IdentifierName(scalarType == null ? syntax + WellKnownDefinitions.NestedTypeSuffix : "ushort"))
                                        )
                                )
                                .ApplyMutationType(context, property.Type, scalarType)
                                .AddRequiredAttribute(classSuffix == WellKnownDefinitions.CreateInputTypeSuffix && property.Type is not NullableTypeSyntax)
                                .WithInitializer(null).WithSemicolonToken(default);
                        }
                    )
                    .Cast<MemberDeclarationSyntax>()
                    .ToArray()
            );

    private static ClassDeclarationSyntax[] GenerateInputClassesDeclaration(GeneratorSyntaxContext context, ClassDeclarationSyntax classDeclaration, string baseClassName, InterfaceDeclarationSyntax inputInterfaceDeclaration) =>
    [
        GenerateInputClassDeclaration(context, classDeclaration, baseClassName, inputInterfaceDeclaration, WellKnownDefinitions.CreateInputTypeSuffix),
        GenerateInputClassDeclaration(context, classDeclaration, baseClassName, inputInterfaceDeclaration, WellKnownDefinitions.UpdateInputTypeSuffix)
    ];

    private static ClassDeclarationSyntax GenerateNestedClassDeclaration(GeneratorSyntaxContext context, ClassDeclarationSyntax classDeclaration, string baseClassName) =>
        SyntaxFactory
            .ClassDeclaration(classDeclaration.Identifier + WellKnownDefinitions.NestedTypeSuffix)
            .WithModifiers(classDeclaration.Modifiers)
            .AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(baseClassName + WellKnownDefinitions.NestedTypeSuffix))
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