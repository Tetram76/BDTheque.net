// ReSharper disable once CheckNamespace
namespace HotChocolate.Types;

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

            if (MappingDefinitions.TypeMappings.FirstOrDefault(mapping => mapping.check(property)).fieldType is { } fieldType)
                descriptor.Field(property).Type(fieldType);
        }

        return descriptor;
    }
}
