namespace BDTheque.Analyzers.Extensions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class PropertyDeclarationSyntaxExtensions
{
    public static bool IsGraphQLReadOnly(this PropertyDeclarationSyntax propertySyntax, GeneratorSyntaxContext context) =>
        propertySyntax.IsAnnotatedWithAttribute("BDTheque.Model.Attributes.GraphQLReadOnlyAttribute", context);

    public static bool IsAnnotatedWithIdType(this PropertyDeclarationSyntax propertySyntax, GeneratorSyntaxContext context) =>
        propertySyntax.IsAnnotatedWithAttribute("HotChocolate.Types.Relay.IDAttribute", context);

    public static bool IsEntityIdProperty(this PropertyDeclarationSyntax propertySyntax, GeneratorSyntaxContext context)
    {
        if (propertySyntax.Parent is not ClassDeclarationSyntax propertySyntaxParent)
            return false;

        const string idSuffix = "Id";
        if (!propertySyntax.Identifier.Text.EndsWith(idSuffix))
            return false;
        string propertyNameWithoutId = propertySyntax.Identifier.Text.Substring(0, propertySyntax.Identifier.Text.Length - idSuffix.Length);

        PropertyDeclarationSyntax? entityProperty = propertySyntaxParent.Members
            .OfType<PropertyDeclarationSyntax>()
            .FirstOrDefault(p => string.Equals(p.Identifier.Text, propertyNameWithoutId, StringComparison.Ordinal));

        return entityProperty != null && entityProperty.Type.IsEntityType(context);
    }

    public static PropertyDeclarationSyntax AddRequiredAttribute(this PropertyDeclarationSyntax propertyDeclarationSyntax, bool isRequired = true)
    {
        if (isRequired)
            propertyDeclarationSyntax = propertyDeclarationSyntax.AddAttributeLists(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("global::System.ComponentModel.DataAnnotations.RequiredAttribute"))
                    )
                )
            );

        return propertyDeclarationSyntax;
    }

    public static PropertyDeclarationSyntax ApplyMutationType(this PropertyDeclarationSyntax propertyDeclarationSyntax, GeneratorSyntaxContext context, TypeSyntax propertyType, TypeSyntax? scalarType)
    {
        if (scalarType != null)
            propertyDeclarationSyntax = propertyDeclarationSyntax.AddAttributeLists(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(
                            SyntaxFactory
                                .GenericName(SyntaxFactory.Identifier("global::HotChocolate.GraphQLTypeAttribute"))
                                .AddTypeArgumentListArguments(scalarType)
                        )
                    )
                )
            );

        return propertyDeclarationSyntax;
    }

    public static IfStatementSyntax BuildApplyTest(this PropertyDeclarationSyntax propertyDeclarationSyntax, bool getterExpression)
    {
        MemberAccessExpressionSyntax condition = SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.ThisExpression(),
                SyntaxFactory.IdentifierName(propertyDeclarationSyntax.Identifier.Text)
            ),
            SyntaxFactory.IdentifierName("HasValue")
        );

        ExpressionSyntax getValueExpression = SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.ThisExpression(),
                SyntaxFactory.IdentifierName(propertyDeclarationSyntax.Identifier.Text)
            ),
            SyntaxFactory.IdentifierName("Value")
        );

        if (getterExpression)
            getValueExpression = SyntaxFactory.AwaitExpression(
                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.IdentifierName("get" + propertyDeclarationSyntax.Identifier.Text),
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Argument(getValueExpression)
                        )
                    )
                )
            );

        AssignmentExpressionSyntax assignment = SyntaxFactory.AssignmentExpression(
            SyntaxKind.SimpleAssignmentExpression,
            SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.IdentifierName("entity"),
                SyntaxFactory.IdentifierName(propertyDeclarationSyntax.Identifier.Text)
            ),
            getValueExpression
        );

        return SyntaxFactory.IfStatement(condition, SyntaxFactory.ExpressionStatement(assignment));
    }

    public static TypeSyntax? GetMutationType(this PropertyDeclarationSyntax propertyDeclarationSyntax, GeneratorSyntaxContext context)
    {
        if (propertyDeclarationSyntax.FindMutationTypeAttribute(context) is not { } mutationAttribute)
            return null;

        if (mutationAttribute.ContainingType.TypeArguments.First() is { } firstArgument)
            return SyntaxFactory.ParseTypeName(firstArgument.ToDisplayString());

        return null;
    }

    private const string MutationTypeMetadataName = "BDTheque.Model.Attributes.MutationTypeAttribute`1";

    public static IMethodSymbol? FindMutationTypeAttribute(this PropertyDeclarationSyntax property, GeneratorSyntaxContext context)
    {
        if (context.SemanticModel.Compilation.GetTypeByMetadataName(MutationTypeMetadataName) is not { } mutationTypeSymbol)
            return null;

        foreach (AttributeSyntax? attribute in property.AttributeLists.SelectMany(attributeList => attributeList.Attributes))
        {
            if (attribute.SemanticModel(context).GetSymbolInfo(attribute).Symbol is not IMethodSymbol attributeSymbol)
                continue;

            if (attributeSymbol.ContainingType.ConstructedFrom.Equals(mutationTypeSymbol, SymbolEqualityComparer.Default))
                return attributeSymbol;
        }

        return null;
    }
}
