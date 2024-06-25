// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;

using BDTheque.Analyzers;

public static class TypeSymbolExtensions
{
    public static IEnumerable<ITypeSymbol> GetHierarchy(this ITypeSymbol typeSymbol, bool includeInterfaces = false)
    {
        yield return typeSymbol;
        if (includeInterfaces)
            foreach (INamedTypeSymbol symbolInterface in typeSymbol.Interfaces)
                yield return symbolInterface;

        foreach (INamedTypeSymbol namedTypeSymbol in typeSymbol.BaseType?.GetHierarchy(includeInterfaces) ?? [])
            yield return namedTypeSymbol;
    }

    public static IEnumerable<ISymbol> GetAllMembers(this ITypeSymbol typeSymbol) =>
        typeSymbol.GetHierarchy().SelectMany(symbol => symbol.GetMembers());

    public static bool IsNullable(this ITypeSymbol typeSymbol) =>
        typeSymbol is INamedTypeSymbol { IsGenericType: true, ConstructedFrom.SpecialType: SpecialType.System_Nullable_T }
        || typeSymbol.NullableAnnotation == NullableAnnotation.Annotated;

    public static bool IsEntityType(this ITypeSymbol typeSymbol, Compilation compilation) =>
        typeSymbol.InheritsFrom(WellKnownDefinitions.BDTheque.Model.Entities.BaseEntity, compilation);

    public static bool IsEntityRepositoryType(this ITypeSymbol typeSymbol, Compilation compilation) =>
        typeSymbol.InheritsFrom(WellKnownDefinitions.BDTheque.Data.Repositories.EntityRepositoryMetadataName, compilation);

    public static bool InheritsFrom(this ITypeSymbol typeSymbol, string parentTypeFullName, Compilation compilation) =>
        compilation.GetTypeByMetadataName(parentTypeFullName) is { } entityTypeSymbol
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

    public static ITypeSymbol EntityKeyTypeSymbol(this ITypeSymbol entityClassSymbol) =>
        entityClassSymbol.GetAllMembers()
            .OfType<IPropertySymbol>()
            .First(symbol => symbol.IsAnnotatedWithAttribute(WellKnownDefinitions.HotChocolate.Types.IdAttribute))
            .Type;
}
