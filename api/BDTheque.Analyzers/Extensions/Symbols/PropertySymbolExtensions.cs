// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;

using BDTheque.Analyzers;

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

    public static ITypeSymbol? GetMutationType(this IPropertySymbol propertySymbol) =>
        propertySymbol.FindMutationTypeAttribute()?.AttributeConstructor is not { } mutationAttributeSymbol
            ? null
            : mutationAttributeSymbol.ContainingType.TypeArguments.FirstOrDefault();

    public static AttributeData? FindMutationTypeAttribute(this IPropertySymbol propertySymbol) =>
        propertySymbol
            .GetAttributes()
            .FirstOrDefault(
                attributeData => attributeData.AttributeClass != null
                                 && attributeData.AttributeClass.FullMetadataName() == WellKnownDefinitions.BDTheque.Model.Attributes.PropertyMutationTypeAttributeMetadataName
            );
}
