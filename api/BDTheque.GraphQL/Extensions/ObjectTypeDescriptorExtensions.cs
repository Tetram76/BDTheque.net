namespace BDTheque.GraphQL.Extensions;

using System.Reflection;
using BDTheque.GraphQL.Scalars;

public static class ObjectTypeDescriptorExtensions
{
    public static readonly IEnumerable<Func<PropertyInfo, bool>> IgnoredProperties =
    [
        property => property.DeclaringType!.GetProperties().Any(info => property.Name == info.Name + "Raw"),
        property => property.DeclaringType!.GetProperties().Any(info => property.Name == info.Name + "Id"),
        property => !property.Name.Equals("Associations", StringComparison.InvariantCultureIgnoreCase) && IsGenericEnumerable(property.PropertyType)
    ];

    public static readonly IEnumerable<(string keyword, Type fieldType)> TypeMappings =
    [
        ("prix", typeof(EuroCurrencyType)),
        ("annee", typeof(YearType)),
        ("mois", typeof(MonthType))
    ];

    public static IObjectTypeDescriptor<T> SetupDefaults<T>(this IObjectTypeDescriptor<T> descriptor)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (IgnoredProperties.Any(func => func(property)))
            {
                descriptor.Field(property).Ignore();
                continue;
            }

            if (property.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                descriptor.Field(property).ID();
            else
                foreach ((string keyword, Type fieldType) mapping in TypeMappings)
                    if (property.Name.Contains(mapping.keyword, StringComparison.InvariantCultureIgnoreCase))
                    {
                        descriptor.Field(property).Type(mapping.fieldType);
                        break;
                    }
        }

        return descriptor;
    }

    private static bool IsGenericEnumerable(Type type)
        => type != typeof(string)
           && type.GetInterfaces()
               .Where(interfaceType => interfaceType.IsGenericType)
               .Select(interfaceType => interfaceType.GetGenericTypeDefinition())
               .Any(genericTypeDefinition => genericTypeDefinition == typeof(IEnumerable<>));
}
