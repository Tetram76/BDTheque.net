namespace BDTheque.Analyzers.Extensions;

using Microsoft.CodeAnalysis;

public static class TypeSymbolExtensions
{
    public static IEnumerable<ITypeSymbol> GetHierarchy(this ITypeSymbol namedTypeSymbol, bool includeInterfaces = false)
    {
        yield return namedTypeSymbol;
        if (includeInterfaces)
            foreach (INamedTypeSymbol baseTypeInterface in namedTypeSymbol.Interfaces)
                yield return baseTypeInterface;

        while (namedTypeSymbol.BaseType is { } baseType)
        {
            yield return baseType;
            if (includeInterfaces)
                foreach (INamedTypeSymbol baseTypeInterface in baseType.Interfaces)
                    yield return baseTypeInterface;

            namedTypeSymbol = baseType;
        }
    }

    public static IEnumerable<ISymbol> GetAllMembers(this ITypeSymbol typeSymbol) =>
        typeSymbol.GetHierarchy().SelectMany(symbol => symbol.GetMembers());

    public static bool IsNullable(this ITypeSymbol typeSymbol) =>
        typeSymbol is INamedTypeSymbol { IsGenericType: true, ConstructedFrom.SpecialType: SpecialType.System_Nullable_T }
        || typeSymbol.NullableAnnotation == NullableAnnotation.Annotated;

    public static bool IsEntityType(this ITypeSymbol typeSymbol, Compilation compilation) =>
        compilation.GetTypeByMetadataName(WellKnownDefinitions.BDTheque.Model.UniqueIdEntity) is { } entityTypeSymbol
        && typeSymbol.GetHierarchy().Any(symbol => SymbolEqualityComparer.Default.Equals(symbol, entityTypeSymbol));

    public static bool IsCollectionType(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol.AllInterfaces.Any(i => i.MetadataName is "ICollection" or "ICollection`1" && i.ContainingNamespace.ToString() == WellKnownDefinitions.System.Collections.Generic.Namespace))
            return true;

        if (typeSymbol is INamedTypeSymbol { IsGenericType: true } namedTypeSymbol)
            return namedTypeSymbol.MetadataName is "ICollection" or "ICollection`1" && namedTypeSymbol.ContainingNamespace.ToString() == WellKnownDefinitions.System.Collections.Generic.Namespace;

        return false;
    }

    public static IEnumerable<IPropertySymbol> MutableProperties(this ITypeSymbol typeSymbol, Compilation compilation) =>
        typeSymbol.GetAllMembers()
            .OfType<IPropertySymbol>()
            .Where(property => property.IsMutable(compilation));

}
