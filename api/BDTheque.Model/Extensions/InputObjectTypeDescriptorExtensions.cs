namespace BDTheque.Model.Extensions;

using System.Reflection;
using BDTheque.Model.Mapping;

public static class InputObjectTypeDescriptorExtensions
{
    public static IInputObjectTypeDescriptor SetupDefaults(this IInputObjectTypeDescriptor descriptor, Type type)
    {
        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (MappingDefinitions.InputIgnoredProperties.Any(func => func(property)))
            {
                descriptor.Field(property).Ignore();
                continue;
            }

            if (property.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                descriptor.Field(property).ID();
            else
                foreach ((string keyword, Type fieldType) mapping in MappingDefinitions.TypeMappings)
                    if (property.Name.Contains(mapping.keyword, StringComparison.InvariantCultureIgnoreCase))
                    {
                        descriptor.Field(property).Type(mapping.fieldType);
                        break;
                    }
        }

        return descriptor;
    }

    public static IInputObjectTypeDescriptor<T> SetupDefaults<T>(this IInputObjectTypeDescriptor<T> descriptor)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (MappingDefinitions.InputIgnoredProperties.Any(func => func(property)))
            {
                descriptor.Field(property).Ignore();
                continue;
            }

            if (property.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                descriptor.Field(property).ID();
            else
                foreach ((string keyword, Type fieldType) mapping in MappingDefinitions.TypeMappings)
                    if (property.Name.Contains(mapping.keyword, StringComparison.InvariantCultureIgnoreCase))
                    {
                        descriptor.Field(property).Type(mapping.fieldType);
                        break;
                    }
        }

        return descriptor;
    }

    private static IInputFieldDescriptor Field(this IInputObjectTypeDescriptor descriptor, PropertyInfo propertyInfo)
        => MappingDefinitions.InvokeField<IInputFieldDescriptor>(descriptor, propertyInfo);

    private static IInputFieldDescriptor Field<T>(this IInputObjectTypeDescriptor<T> descriptor, PropertyInfo propertyInfo)
        => MappingDefinitions.InvokeField<IInputFieldDescriptor>(descriptor, propertyInfo);
}
