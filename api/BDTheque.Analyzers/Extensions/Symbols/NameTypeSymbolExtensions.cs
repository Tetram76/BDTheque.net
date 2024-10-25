// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;

public static class NameTypeSymbolExtensions
{
    public static IEnumerable<INamedTypeSymbol> GetHierarchy(this INamedTypeSymbol namedTypeSymbol, bool includeInterfaces = false)
    {
        yield return namedTypeSymbol;
        if (includeInterfaces)
            foreach (INamedTypeSymbol symbolInterface in namedTypeSymbol.Interfaces)
                yield return symbolInterface;

        while (namedTypeSymbol.BaseType is { } baseType)
        {
            yield return baseType;
            if (includeInterfaces)
                foreach (INamedTypeSymbol baseTypeInterface in baseType.Interfaces)
                    yield return baseTypeInterface;

            namedTypeSymbol = baseType;
        }
    }

    public static IEnumerable<ISymbol> GetAllMembers(this INamedTypeSymbol typeSymbol) =>
        typeSymbol.GetHierarchy().SelectMany(symbol => symbol.GetMembers());
}
