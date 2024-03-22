namespace BDTheque.Analyzers.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

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
        var symbolsToCheck = new Queue<ISymbol>(
            new[]
            {
                symbol
            }
        );

        while (symbolsToCheck.Any())
        {
            ISymbol? currentSymbol = symbolsToCheck.Dequeue();

            attributeData = currentSymbol
                .GetAttributes()
                .FirstOrDefault(attr => attr.AttributeClass?.FullMetadataName() == attributeClassFullname);

            if (attributeData != null)
                return true;

            if (currentSymbol is not INamedTypeSymbol namedTypeSymbol)
                continue;

            INamedTypeSymbol? baseType = namedTypeSymbol.BaseType;
            if (baseType != null)
                symbolsToCheck.Enqueue(baseType);

            foreach (INamedTypeSymbol implementedInterface in namedTypeSymbol.Interfaces)
                symbolsToCheck.Enqueue(implementedInterface);
        }

        attributeData = null;
        return false;
    }
}
