namespace BDTheque.Model.Mapping;

using System.Linq.Expressions;
using System.Reflection;
using BDTheque.Model.Scalars;

public static class MappingDefinitions
{
    public static readonly IEnumerable<Func<PropertyInfo, bool>> ObjectIgnoredProperties =
    [
        property => property.DeclaringType!.GetProperties().Any(info => property.Name == info.Name + "Raw"),
        property => property.DeclaringType!.GetProperties().Any(info => property.Name == info.Name + "Id"),
        property => !property.Name.Equals("Associations", StringComparison.InvariantCultureIgnoreCase) && IsGenericEnumerable(property.PropertyType)
    ];

    public static readonly IEnumerable<Func<PropertyInfo, bool>> InputIgnoredProperties =
        ObjectIgnoredProperties.Union(
            [
                property => property.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase),
                property => property.Name.Equals("CreatedAt", StringComparison.InvariantCultureIgnoreCase),
                property => property.Name.Equals("UpdatedAt", StringComparison.InvariantCultureIgnoreCase),
                property => property.Name.Equals("Initiale", StringComparison.InvariantCultureIgnoreCase)
            ]
        );

    public static readonly IEnumerable<(string keyword, Type fieldType)> TypeMappings =
    [
        ("prix", typeof(EuroCurrencyType)),
        ("annee", typeof(YearType)),
        ("mois", typeof(MonthType))
    ];

    private static bool IsGenericEnumerable(Type type)
        => type != typeof(string)
           && type.GetInterfaces()
               .Where(interfaceType => interfaceType.IsGenericType)
               .Select(interfaceType => interfaceType.GetGenericTypeDefinition())
               .Any(genericTypeDefinition => genericTypeDefinition == typeof(IEnumerable<>));

    public static LambdaExpression GetPropertySelector(PropertyInfo propertyInfo, Type propertyType)
    {
        ParameterExpression parameter = Expression.Parameter(propertyInfo.ReflectedType!, "o");
        UnaryExpression propertyAccess = Expression.Convert(Expression.MakeMemberAccess(parameter, propertyInfo), propertyType);
        Type lambdaExpressionType = typeof(Func<,>).MakeGenericType(propertyInfo.ReflectedType!, propertyType);
        LambdaExpression lambdaExpression = Expression.Lambda(lambdaExpressionType, propertyAccess, parameter);
        return lambdaExpression;
    }


    public static TFieldDescriptor InvokeField<TFieldDescriptor>(object descriptor, PropertyInfo propertyInfo)
    {
        // Trouver la méthode générique Field avec les paramètres spécifiques
        Type propertyType = propertyInfo.PropertyType;
        MethodInfo? fieldMethod = descriptor.GetType()
            .GetMethods()
            .FirstOrDefault(
                m => m is { Name: "Field", IsGenericMethod: true } &&
                     m.GetParameters().Length == 1 &&
                     m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>)
            );

        if (fieldMethod != null)
        {
            fieldMethod = fieldMethod.MakeGenericMethod(propertyInfo.PropertyType);
        }
        else
        {
            propertyType = typeof(object);
            fieldMethod = descriptor.GetType()
                .GetMethods()
                .FirstOrDefault(
                    m => m is { Name: "Field", IsGenericMethod: false } &&
                         m.GetParameters().Length == 1 &&
                         m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>)
                );
        }

        if (fieldMethod == null)
            throw new InvalidOperationException("La méthode Field n'a pas été trouvée.");

        // Invoquer dynamiquement la méthode Field avec l'expression lambda et capturer le résultat
        try
        {
            return (TFieldDescriptor) fieldMethod.Invoke(descriptor, [GetPropertySelector(propertyInfo, propertyType)])!;
        }
        catch (TargetInvocationException ex)
        {
            // Gérer ou propager l'exception selon vos besoins
            throw new InvalidOperationException("L'invocation de la méthode Field a échoué", ex);
        }
    }
}
