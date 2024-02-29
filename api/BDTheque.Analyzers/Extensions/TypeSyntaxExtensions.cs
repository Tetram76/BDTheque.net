namespace BDTheque.Analyzers.Extensions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class TypeSyntaxExtensions
{
    public static bool IsEntityType(this TypeSyntax typeSyntax, GeneratorSyntaxContext context)
    {
        var semanticModel = typeSyntax.SemanticModel(context);
        if (semanticModel.Compilation.GetTypeByMetadataName("BDTheque.Model.Entities.Abstract.VersioningEntity") is not { } entityTypeSymbol) return false;
        if (ModelExtensions.GetTypeInfo(semanticModel, typeSyntax).ConvertedType is not { } typeSymbol) return false;

        INamedTypeSymbol? baseClassSymbol = typeSymbol.BaseType;
        while (baseClassSymbol != null)
        {
            if (SymbolEqualityComparer.Default.Equals(baseClassSymbol, entityTypeSymbol))
                return true;
            baseClassSymbol = baseClassSymbol.BaseType;
        }

        return false;
    }

    public static bool IsCollectionType(this TypeSyntax typeSyntax, GeneratorSyntaxContext context)
    {
        if (typeSyntax.SemanticModel(context).GetTypeInfo(typeSyntax).ConvertedType is not { } typeSymbol) return false;

        if (typeSymbol.AllInterfaces.Any(i => i.MetadataName is "ICollection" or "ICollection`1" && i.ContainingNamespace.ToString() == "System.Collections.Generic"))
            return true;

        if (typeSymbol is INamedTypeSymbol { IsGenericType: true } namedTypeSymbol)
            return namedTypeSymbol.MetadataName is "ICollection" or "ICollection`1" && namedTypeSymbol.ContainingNamespace.ToString() == "System.Collections.Generic";

        return false;
    }

    public static TypeSyntax RewriteType(this TypeSyntax type, GeneratorSyntaxContext context, Func<TypeSyntax, TypeSyntax> rewrite) =>
        type switch
        {
            NullableTypeSyntax nullableTypeSyntax =>
                nullableTypeSyntax.WithElementType(nullableTypeSyntax.ElementType.RewriteType(context, rewrite)),
            ArrayTypeSyntax arrayTypeSyntax =>
                arrayTypeSyntax.WithElementType(arrayTypeSyntax.ElementType.RewriteType(context, rewrite)),
            GenericNameSyntax genericNameSyntax =>
                genericNameSyntax.WithTypeArgumentList( // not AddTypeArgumentListArguments: we have to replace the original list
                    SyntaxFactory.TypeArgumentList(
                        SyntaxFactory.SeparatedList(
                            genericNameSyntax.TypeArgumentList.Arguments.Select(syntax => syntax.RewriteType(context, rewrite))
                        )
                    )
                ),
            _ =>
                type.IsEntityType(context) ? rewrite(type) : type
        };

}
