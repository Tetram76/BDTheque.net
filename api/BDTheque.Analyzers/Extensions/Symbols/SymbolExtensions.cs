// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;

using System.Diagnostics.CodeAnalysis;

public static class SymbolExtensions
{
    public static string FullMetadataName(this ISymbol symbol)
    {
        string metadataName = symbol.MetadataName;

        INamedTypeSymbol? containingType = symbol.ContainingType;
        while (containingType is not null)
        {
            metadataName = $"{containingType.MetadataName}.{metadataName}";
            containingType = containingType.ContainingType;
        }

        INamespaceSymbol? containingNamespace = symbol.ContainingNamespace;
        while (containingNamespace is not null && !containingNamespace.IsGlobalNamespace)
        {
            metadataName = $"{containingNamespace.MetadataName}.{metadataName}";
            containingNamespace = containingNamespace.ContainingNamespace;
        }

        return metadataName;
    }

    public static bool IsAnnotatedWithAttribute(this ISymbol symbol, string attributeClassFullname) =>
        symbol.IsAnnotatedWithAttribute(attributeClassFullname, out _);

    public static bool IsAnnotatedWithAttribute(this ISymbol symbol, string attributeClassFullname, [NotNullWhen(true)] out AttributeData? attributeData)
    {
        IEnumerable<ISymbol> symbols = symbol switch
        {
            INamedTypeSymbol namedTypeSymbol => namedTypeSymbol.GetHierarchy(true),
            ITypeSymbol typeSymbol => typeSymbol.GetHierarchy(true),
            _ => [symbol]
        };

        attributeData = symbols
            .SelectMany(s => s.GetAttributes())
            .FirstOrDefault(attr => attr.AttributeClass?.FullMetadataName() == attributeClassFullname);
        return attributeData != null;
    }
}
