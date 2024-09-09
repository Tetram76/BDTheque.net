// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis.CSharp.Syntax;

using BDTheque.Analyzers;

public static class PropertyDeclarationSyntaxExtensions
{
    public static bool IsMutable(this PropertyDeclarationSyntax propertySyntax, GeneratorSyntaxContext context)
    {
        SemanticModel semanticModel = propertySyntax.SemanticModel(context);
        return semanticModel.GetDeclaredSymbol(propertySyntax)!.IsMutable(semanticModel.Compilation);
    }

    public static bool IsAnnotatedWithIdType(this PropertyDeclarationSyntax propertySyntax, GeneratorSyntaxContext context) =>
        propertySyntax.IsAnnotatedWithAttribute(WellKnownDefinitions.HotChocolate.Types.IdAttribute, context);

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
        if (propertyDeclarationSyntax.FindMutationTypeAttribute(context)?.AttributeConstructor is not { } mutationAttributeSymbol)
            return null;

        if (mutationAttributeSymbol.ContainingType.TypeArguments.First() is { } firstArgument)
            return SyntaxFactory.ParseTypeName(firstArgument.ToDisplayString());

        return null;
    }

    public static AttributeData? FindMutationTypeAttribute(this PropertyDeclarationSyntax property, GeneratorSyntaxContext context)
    {
        SemanticModel semanticModel = property.SemanticModel(context);
        return semanticModel.GetDeclaredSymbol(property)!.FindMutationTypeAttribute();
    }
}
