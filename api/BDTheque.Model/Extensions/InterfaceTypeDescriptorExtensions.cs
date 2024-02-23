namespace BDTheque.Model.Extensions;

using System.Reflection;
using BDTheque.Model.Mapping;
using HotChocolate.Types.Descriptors;

public static class InterfaceTypeDescriptorExtensions
{
    public static IInterfaceTypeDescriptor SetupDefaults(this IInterfaceTypeDescriptor descriptor, Type type)
    {
        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (MappingDefinitions.ObjectIgnoredProperties.Any(func => func(property)))
            {
                descriptor.Field(property).Ignore();
                continue;
            }

            if (property.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                descriptor.Field(property).ID();
            else if (MappingDefinitions.TypeMappings.FirstOrDefault(mapping => mapping.check(property)).fieldType is {} fieldType)
                descriptor.Field(property).Type(fieldType);
        }

        return descriptor;
    }

    private static InterfaceFieldDescriptor Field(this IInterfaceTypeDescriptor descriptor, PropertyInfo propertyInfo)
        => MappingDefinitions.InvokeField<InterfaceFieldDescriptor>(descriptor, propertyInfo);
}
