namespace BDTheque.Model.Mapping;

using System.Linq.Expressions;
using System.Reflection;

using BDTheque.Model.Scalars;

public static class MappingDefinitions
{
    public static readonly IEnumerable<Func<PropertyInfo, bool>> ObjectIgnoredProperties =
    [
        property => property.Name.EndsWith("Char", StringComparison.InvariantCulture),
        property => property.DeclaringType!.GetProperties().Exists(p => property.Name == p.Name + "Raw"),
        property => property.DeclaringType!.GetProperties().Exists(p => property.Name == p.Name + "Id"),
        property => property.Name.Equals("Bytes", StringComparison.InvariantCultureIgnoreCase),
        property => !property.Name.Equals("Associations", StringComparison.InvariantCultureIgnoreCase) && IsGenericEnumerable(property.PropertyType)
    ];

    public static readonly IEnumerable<Func<PropertyInfo, bool>> MutationInputIgnoredProperties =
    [
        ..ObjectIgnoredProperties,
        // property => property.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase),
        property => property.Name.Equals("CreatedAt", StringComparison.InvariantCultureIgnoreCase),
        property => property.Name.Equals("UpdatedAt", StringComparison.InvariantCultureIgnoreCase),
        property => property.Name.Equals("Initiale", StringComparison.InvariantCultureIgnoreCase)
    ];

    public static readonly IEnumerable<(Func<PropertyInfo, bool> check, Type fieldType)> TypeMappings =
    [
        (property => property.Name.Contains("prix", StringComparison.InvariantCultureIgnoreCase), typeof(EuroCurrencyType)),
        (property => property.GetCustomAttribute<NonNegativeAttribute<decimal>>() != null, typeof(NonNegativeFloatType)),
        (property => property.GetCustomAttribute<YearAttribute>() != null, typeof(YearType)),
        (property => property.GetCustomAttribute<MonthAttribute>() != null, typeof(MonthType))
    ];

    public static readonly IEnumerable<(Func<PropertyInfo, bool> check, Type fieldType)> MutationTypeMappings =
    [
        ..TypeMappings
    ];

    private static bool IsGenericEnumerable(Type type)
        => type != typeof(string)
           && type.GetInterfaces()
               .Where(interfaceType => interfaceType.IsGenericType)
               .Select(interfaceType => interfaceType.GetGenericTypeDefinition())
               .Any(
                   genericTypeDefinition =>
                   {
                       if (genericTypeDefinition == typeof(IEnumerable<>))
                           return true;
                       if (genericTypeDefinition == typeof(Optional<>))
                           return IsGenericEnumerable(genericTypeDefinition);

                       return false;
                   }
               );

    public static LambdaExpression GetPropertySelector(PropertyInfo propertyInfo, Type propertyType)
    {
        ParameterExpression parameter = Expression.Parameter(propertyInfo.ReflectedType!, "o");
        Expression propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
        if (propertyType != propertyInfo.PropertyType)
            propertyAccess = Expression.Convert(propertyAccess, propertyType); // /!\ not EF compliant
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
            .Find(
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
                .Find(
                    m => m is { Name: "Field", IsGenericMethod: false } &&
                         m.GetParameters().Length == 1 &&
                         m.GetParameters()[0].ParameterType.IsGenericType &&
                         m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>)
                );
        }

        if (fieldMethod == null)
            return InvokeFieldWithName<TFieldDescriptor>(descriptor, propertyInfo);

        // Invoquer dynamiquement la méthode Field avec l'expression lambda et capturer le résultat
        try
        {
            return (TFieldDescriptor) fieldMethod.Invoke(descriptor, [GetPropertySelector(propertyInfo, propertyType)])!;
        }
        catch (TargetInvocationException ex)
        {
            // Gérer ou propager l'exception selon vos besoins
            throw new InvalidOperationException("L'invocation de la méthode Field<> a échoué", ex);
        }
    }

    private static TFieldDescriptor InvokeFieldWithName<TFieldDescriptor>(object descriptor, PropertyInfo propertyInfo)
    {
        MethodInfo? fieldMethod = descriptor.GetType()
            .GetMethods()
            .Find(
                m => m is { Name: "Field", IsGenericMethod: false } &&
                     m.GetParameters().Length == 1 &&
                     m.GetParameters()[0].ParameterType == typeof(string)
            );

        if (fieldMethod == null)
            throw new InvalidOperationException("La méthode Field n'a pas été trouvée.");

        try
        {
            return (TFieldDescriptor) fieldMethod.Invoke(descriptor, [propertyInfo.Name])!;
        }
        catch (TargetInvocationException ex)
        {
            // Gérer ou propager l'exception selon vos besoins
            throw new InvalidOperationException("L'invocation de la méthode Field a échoué", ex);
        }
    }
}
