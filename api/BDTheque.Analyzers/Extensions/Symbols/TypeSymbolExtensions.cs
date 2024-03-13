namespace BDTheque.Analyzers.Extensions;

using Microsoft.CodeAnalysis;

public static class TypeSymbolExtensions
{
    public static bool IsNullable(this ITypeSymbol typeSymbol) =>
        typeSymbol is INamedTypeSymbol { IsGenericType: true, ConstructedFrom.SpecialType: SpecialType.System_Nullable_T }
        || typeSymbol.NullableAnnotation == NullableAnnotation.Annotated;

    public static bool IsEntityType(this ITypeSymbol typeSymbol, Compilation compilation)
    {
        if (compilation.GetTypeByMetadataName(WellKnownDefinitions.BDTheque.Model.VersioningEntity) is not { } entityTypeSymbol)
            return false;

        INamedTypeSymbol? baseClassSymbol = typeSymbol.BaseType;
        while (baseClassSymbol != null)
        {
            if (SymbolEqualityComparer.Default.Equals(baseClassSymbol, entityTypeSymbol))
                return true;
            baseClassSymbol = baseClassSymbol.BaseType;
        }

        return false;
    }

    public static bool IsCollectionType(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol.AllInterfaces.Any(i => i.MetadataName is "ICollection" or "ICollection`1" && i.ContainingNamespace.ToString() == WellKnownDefinitions.System.Collections.Generic.Namespace))
            return true;

        if (typeSymbol is INamedTypeSymbol { IsGenericType: true } namedTypeSymbol)
            return namedTypeSymbol.MetadataName is "ICollection" or "ICollection`1" && namedTypeSymbol.ContainingNamespace.ToString() == WellKnownDefinitions.System.Collections.Generic.Namespace;

        return false;
    }
}
