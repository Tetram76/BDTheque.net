namespace BDTheque.Model.Extensions;

using System.Reflection;
using BDTheque.Model.Mapping;

public static class ObjectTypeDescriptorExtensions
{
    public static IObjectTypeDescriptor SetupDefaults(this IObjectTypeDescriptor descriptor, Type type)
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
}
