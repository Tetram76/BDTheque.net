namespace BDTheque.Analyzers.Extensions;

using Microsoft.CodeAnalysis;

public static class NamespaceOrTypeSymbolExtensions
{
    public static IEnumerable<IPropertySymbol> MutableProperties(this INamespaceOrTypeSymbol namespaceOrTypeSymbol, Compilation compilation) =>
        namespaceOrTypeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(property => property.IsMutable(compilation));
}
