namespace BDTheque.GraphQL.Extensions;

using System.Linq.Expressions;
using System.Reflection;
using BDTheque.GraphQL.Inputs;
using BDTheque.Model.Interfaces;

public static class InputObjectTypeDescriptorExtensions
{
    public static readonly IEnumerable<Func<PropertyInfo, bool>> IgnoredProperties =
        ObjectTypeDescriptorExtensions.IgnoredProperties.Union(
            [
                property => property.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase),
                property => property.Name.Equals("CreatedAt", StringComparison.InvariantCultureIgnoreCase),
                property => property.Name.Equals("UpdatedAt", StringComparison.InvariantCultureIgnoreCase),
                property => property.Name.Equals("Initiale", StringComparison.InvariantCultureIgnoreCase)
            ]
        );

    public static IInputObjectTypeDescriptor<T> SetupDefaults<T>(this IInputObjectTypeDescriptor<T> descriptor)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (IgnoredProperties.Any(func => func(property)))
            {
                descriptor.Field(property).Ignore();
                continue;
            }

            foreach ((string keyword, Type fieldType) mapping in ObjectTypeDescriptorExtensions.TypeMappings)
                if (property.Name.Contains(mapping.keyword, StringComparison.InvariantCultureIgnoreCase))
                {
                    descriptor.Field(property).Type(mapping.fieldType);
                    break;
                }
        }

        return descriptor;
    }

    private static IInputFieldDescriptor Field<T>(this IInputObjectTypeDescriptor<T> descriptor, PropertyInfo propertyInfo)
    {
        // Trouver la méthode générique Field avec les paramètres spécifiques
        MethodInfo? fieldMethod = typeof(IInputObjectTypeDescriptor<T>)
            .GetMethods()
            .FirstOrDefault(
                m => m is { Name: "Field", IsGenericMethod: true } &&
                     m.GetParameters().Length == 1 &&
                     m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>)
            );

        if (fieldMethod == null)
            throw new InvalidOperationException("La méthode Field n'a pas été trouvée.");

        fieldMethod = fieldMethod.MakeGenericMethod(propertyInfo.PropertyType);

        // Invoquer dynamiquement la méthode Field avec l'expression lambda et capturer le résultat
        try
        {
            return (IInputFieldDescriptor) fieldMethod.Invoke(descriptor, [GetPropertySelector<T>(propertyInfo)])!;
        }
        catch (TargetInvocationException ex)
        {
            // Gérer ou propager l'exception selon vos besoins
            throw new InvalidOperationException("L'invocation de la méthode Field a échoué", ex);
        }
    }

    private static LambdaExpression GetPropertySelector<T>(PropertyInfo propertyInfo)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), "o");
        MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
        Type lambdaExpressionType = typeof(Func<,>).MakeGenericType(typeof(T), propertyInfo.PropertyType);
        LambdaExpression lambdaExpression = Expression.Lambda(lambdaExpressionType, propertyAccess, parameter);
        return lambdaExpression;
    }
}
