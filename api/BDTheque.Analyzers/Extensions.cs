namespace BDTheque.Analyzers;

using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class Extensions
{
    public static ClassDeclarationSyntax AddGeneratedAttribute<T>(this ClassDeclarationSyntax classDeclarationSyntax) =>
        classDeclarationSyntax.AddAttributeLists(
            SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Attribute(
                        SyntaxFactory.IdentifierName("global::" + typeof(GeneratedCodeAttribute).FullName),
                        SyntaxFactory.AttributeArgumentList(
                            SyntaxFactory.SeparatedList(
                                [
                                    SyntaxFactory.AttributeArgument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(typeof(T).FullName!))),
                                    SyntaxFactory.AttributeArgument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(typeof(T).Assembly.GetName().Version.ToString())))
                                ]
                            )
                        )
                    )
                )
            )
        );

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

    private static bool IsEntityType(this GeneratorSyntaxContext context, TypeSyntax typeSyntax)
    {
        ITypeSymbol? typeSymbol = context.SemanticModel.GetTypeInfo(typeSyntax).Type;
        INamedTypeSymbol? baseClassSymbol = typeSymbol?.BaseType;
        while (baseClassSymbol != null)
        {
            if (baseClassSymbol.MetadataName == "VersioningEntity")
                return true;
            baseClassSymbol = baseClassSymbol.BaseType;
        }

        return false;
    }

    public static bool IsCollectionType(this GeneratorSyntaxContext context, TypeSyntax typeSyntax)
    {
        if (context.SemanticModel.GetTypeInfo(typeSyntax).ConvertedType is not { } typeSymbol) return false;

        if (typeSymbol.AllInterfaces.Any(i => i.MetadataName is "ICollection" or "ICollection`1" && i.ContainingNamespace.ToString() == "System.Collections.Generic"))
            return true;

        if (typeSymbol is INamedTypeSymbol { IsGenericType: true } namedTypeSymbol)
            return namedTypeSymbol.MetadataName is "ICollection" or "ICollection`1" && namedTypeSymbol.ContainingNamespace.ToString() == "System.Collections.Generic";

        return false;
    }

    public static TypeSyntax RewriteType(this GeneratorSyntaxContext context, TypeSyntax propertyType, Func<TypeSyntax, TypeSyntax> rewrite) =>
        propertyType switch
        {
            NullableTypeSyntax nullableTypeSyntax =>
                nullableTypeSyntax.WithElementType(RewriteType(context, nullableTypeSyntax.ElementType, rewrite)),
            ArrayTypeSyntax arrayTypeSyntax =>
                arrayTypeSyntax.WithElementType(RewriteType(context, arrayTypeSyntax.ElementType, rewrite)),
            GenericNameSyntax genericNameSyntax =>
                genericNameSyntax.WithTypeArgumentList( // not AddTypeArgumentListArguments: we have to replace the original list
                    SyntaxFactory.TypeArgumentList(
                        SyntaxFactory.SeparatedList(
                            genericNameSyntax.TypeArgumentList.Arguments.Select(syntax => RewriteType(context, syntax, rewrite))
                        )
                    )
                ),
            _ =>
                context.IsEntityType(propertyType) ? rewrite(propertyType) : propertyType
        };

    public static bool IsEntityIdProperty(this GeneratorSyntaxContext context, PropertyDeclarationSyntax propertySyntax)
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

        return entityProperty != null && IsEntityType(context, entityProperty.Type);
    }
}
