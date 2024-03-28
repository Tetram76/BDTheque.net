namespace BDTheque.Model.Extensions;

using System.Reflection;

using BDTheque.Model.Mapping;

using HotChocolate.Data.Filters;

public static class FilterInputTypeDescriptorExtensions
{
    public static IFilterInputTypeDescriptor SetupDefaults(this IFilterInputTypeDescriptor descriptor, Type type)
    {
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo property in properties.Where(info => MappingDefinitions.ObjectIgnoredProperties.Any(func => func(info))))
            descriptor.Field(property).Ignore();

        return descriptor;
    }

    public static IFilterInputTypeDescriptor<T> SetupDefaults<T>(this IFilterInputTypeDescriptor<T> descriptor)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties.Where(info => MappingDefinitions.ObjectIgnoredProperties.Any(func => func(info))))
            descriptor.Field(property).Ignore();

        return descriptor;
    }

    private static IFilterFieldDescriptor Field(this IFilterInputTypeDescriptor descriptor, PropertyInfo propertyInfo)
        => MappingDefinitions.InvokeField<IFilterFieldDescriptor>(descriptor, propertyInfo);

    private static IFilterFieldDescriptor Field<T>(this IFilterInputTypeDescriptor<T> descriptor, PropertyInfo propertyInfo)
        => MappingDefinitions.InvokeField<IFilterFieldDescriptor>(descriptor, propertyInfo);
}
