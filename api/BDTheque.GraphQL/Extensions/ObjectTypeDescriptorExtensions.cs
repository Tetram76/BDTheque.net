namespace BDTheque.GraphQL.Extensions;

using System.Reflection;

public static class ObjectTypeDescriptorExtensions
{
    public static IObjectTypeDescriptor<T> SetupDefaults<T>(this IObjectTypeDescriptor<T> descriptor)
    {
        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        {
        }

        return descriptor;
    }
}
