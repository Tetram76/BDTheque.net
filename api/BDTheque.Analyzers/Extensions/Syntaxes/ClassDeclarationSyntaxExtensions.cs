// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis.CSharp.Syntax;

using BDTheque.Analyzers;
using BDTheque.Analyzers.Helpers;

using Microsoft.CodeAnalysis.Diagnostics;

public static class ClassDeclarationSyntaxExtensions
{
    public static bool IsAnnotatedWithObjectType(this ClassDeclarationSyntax classDeclarationSyntax, GeneratorSyntaxContext context) =>
        classDeclarationSyntax.IsAnnotatedWithAttribute(WellKnownDefinitions.HotChocolate.Types.ObjectTypeAttribute, context);

    public static bool IsAnnotatedWithMutationType(this ClassDeclarationSyntax classDeclarationSyntax, GeneratorSyntaxContext context) =>
        classDeclarationSyntax.IsAnnotatedWithAttribute(WellKnownDefinitions.BDTheque.GraphQL.Attributes.MutationEntityAttributeMetadataName, context);

    public static bool IsAnnotatedWithMutationType(this ClassDeclarationSyntax classDeclarationSyntax, SyntaxNodeAnalysisContext context) =>
        classDeclarationSyntax.IsAnnotatedWithAttribute(WellKnownDefinitions.BDTheque.GraphQL.Attributes.MutationEntityAttributeMetadataName, context);

    public static bool IsAnnotatedWithStaticEntityAttribute(this ClassDeclarationSyntax classDeclarationSyntax, GeneratorSyntaxContext context) =>
        classDeclarationSyntax.IsAnnotatedWithAttribute(WellKnownDefinitions.BDTheque.Model.Attributes.StaticEntityAttribute, context);

    public static bool IsAnnotatedWithEntityRepository(this ClassDeclarationSyntax classDeclarationSyntax, GeneratorSyntaxContext context) =>
        classDeclarationSyntax.IsAnnotatedWithAttribute(WellKnownDefinitions.BDTheque.Data.Attributes.EntityRepositoryAttribute, context);

    public static ITypeSymbol GetEntityTypeFromAttribute(this ClassDeclarationSyntax classDeclarationSyntax, GeneratorSyntaxContext context)
    {
        if (!classDeclarationSyntax.IsAnnotatedWithAttribute(WellKnownDefinitions.BDTheque.GraphQL.Attributes.MutationEntityAttributeMetadataName, classDeclarationSyntax.SemanticModel(context), out AttributeSyntax? attributeSyntax))
            throw new InvalidOperationException($"Expected {classDeclarationSyntax.Identifier} to be annotated with {WellKnownDefinitions.BDTheque.GraphQL.Attributes.MutationEntityAttributeMetadataName} attribute but is not.");

        SymbolInfo attributeSymbolInfo = attributeSyntax.SemanticModel(context).GetSymbolInfo(attributeSyntax);
        if (attributeSymbolInfo.Symbol is not IMethodSymbol attributeSymbol)
            throw new InvalidOperationException("Expected a Method symbol but found none.");

        if (attributeSymbol.ContainingType is not { IsGenericType: true } attributeClassSymbol || attributeClassSymbol.TypeArguments.Length == 0)
            throw new InvalidOperationException("Attribute is supposed to be generic but is not.");

        return attributeClassSymbol.TypeArguments[0];
    }

    public static IEnumerable<MemberDeclarationSyntax> AllMembers(this ClassDeclarationSyntax classDeclaration, GeneratorSyntaxContext context)
    {
        var members = classDeclaration.Members.ToList();
        if (classDeclaration.SemanticModel(context).GetDeclaredSymbol(classDeclaration) is not { } classSymbol)
            return members;

        var baseTypeSymbol = classSymbol.BaseType;
        while (baseTypeSymbol != null && baseTypeSymbol.SpecialType != SpecialType.System_Object)
        {
            var baseTypeDeclarations = baseTypeSymbol.DeclaringSyntaxReferences.Select(sr => sr.GetSyntax()).OfType<ClassDeclarationSyntax>();
            foreach (var baseTypeDeclaration in baseTypeDeclarations)
            {
                members.AddRange(baseTypeDeclaration.Members);
            }

            baseTypeSymbol = baseTypeSymbol.BaseType;
        }

        return members;
    }

    public static IEnumerable<PropertyDeclarationSyntax> MutableProperties(this ClassDeclarationSyntax classDeclarationSyntax, GeneratorSyntaxContext context) =>
        classDeclarationSyntax.Members
            .OfType<PropertyDeclarationSyntax>()
            .Where(property => property.IsMutable(context));

    public static ClassDeclarationSyntax AddGeneratedAttribute<T>(this ClassDeclarationSyntax classDeclarationSyntax) =>
        classDeclarationSyntax.AddAttributeLists(AttributeListSyntaxHelper.GeneratedAttributeList<T>());

    public static ClassDeclarationSyntax AddImplicitConverter(this ClassDeclarationSyntax classDeclarationSyntax, PropertyDeclarationSyntax? idProperty)
    {
        if (idProperty == null)
            return classDeclarationSyntax;

        var classNameSyntax = SyntaxFactory.IdentifierName(classDeclarationSyntax.Identifier.Text);
        string parameterName = idProperty.Identifier.Text.ToCamelCase();

        classDeclarationSyntax = classDeclarationSyntax.AddMembers(
            SyntaxFactory
                .ConversionOperatorDeclaration(SyntaxFactory.Token(SyntaxKind.ImplicitKeyword), classNameSyntax)
                .AddModifiers(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                    SyntaxFactory.Token(SyntaxKind.StaticKeyword)
                )
                .AddParameterListParameters(
                    SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameterName)).WithType(idProperty.Type)
                )
                .WithBody(
                    SyntaxFactory.Block(
                        SyntaxFactory.SingletonList<StatementSyntax>(
                            SyntaxFactory.ReturnStatement(
                                SyntaxFactory.ObjectCreationExpression(classNameSyntax)
                                    .WithInitializer(
                                        SyntaxFactory.InitializerExpression(SyntaxKind.ObjectInitializerExpression)
                                            .AddExpressions(
                                                SyntaxFactory.AssignmentExpression(
                                                    SyntaxKind.SimpleAssignmentExpression,
                                                    SyntaxFactory.IdentifierName(idProperty.Identifier.Text),
                                                    SyntaxFactory.IdentifierName(parameterName)
                                                )
                                            )
                                    )
                            )
                        )
                    )
                )
        );

        return classDeclarationSyntax;
    }
}
