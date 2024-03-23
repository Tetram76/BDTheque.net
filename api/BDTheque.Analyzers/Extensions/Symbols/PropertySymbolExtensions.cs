namespace BDTheque.Analyzers.Extensions;

using Microsoft.CodeAnalysis;

public static class PropertySymbolExtensions
{
    public static bool IsMutable(this IPropertySymbol propertySymbol, Compilation compilation) =>
        !propertySymbol.Type.IsCollectionType()
        && !propertySymbol.IsEntityIdProperty(compilation)
        && !propertySymbol.Name.EndsWith("Raw")
        && !propertySymbol.IsGraphQLReadOnly();

    public static bool IsGraphQLReadOnly(this IPropertySymbol propertySymbol) =>
        propertySymbol.IsAnnotatedWithAttribute(WellKnownDefinitions.BDTheque.Model.Attributes.GraphQLReadOnlyAttribute);

    public static bool IsEntityIdProperty(this IPropertySymbol propertySymbol, Compilation compilation)
    {
        if (propertySymbol.ContainingType is not ITypeSymbol parentSymbol)
            return false;

        const string idSuffix = "Id";
        if (!propertySymbol.Name.EndsWith(idSuffix))
            return false;
        string propertyNameWithoutId = propertySymbol.Name[..^idSuffix.Length];

        IPropertySymbol? entityProperty = parentSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .FirstOrDefault(p => string.Equals(p.Name, propertyNameWithoutId, StringComparison.Ordinal));

        return entityProperty != null && entityProperty.Type.IsEntityType(compilation);
    }

    public static ITypeSymbol? GetMutationType(this IPropertySymbol propertySymbol, Compilation compilation) =>
        propertySymbol.FindMutationTypeAttribute(compilation) is not { } mutationAttributeSymbol
            ? null
            : mutationAttributeSymbol.ContainingType.TypeArguments.FirstOrDefault();

    public static IMethodSymbol? FindMutationTypeAttribute(this IPropertySymbol propertySymbol, Compilation compilation) =>
        propertySymbol.GetAttributes()
            .Where(
                attributeData => attributeData.AttributeClass != null
                                 && attributeData.AttributeClass.FullMetadataName() == WellKnownDefinitions.BDTheque.Model.Attributes.PropertyMutationTypeAttribute
            )
            .Select(attributeData => attributeData.AttributeConstructor)
            .FirstOrDefault();
}
