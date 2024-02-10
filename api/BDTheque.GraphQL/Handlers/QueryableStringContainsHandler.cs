namespace BDTheque.GraphQL.Handlers;

using System.Linq.Expressions;
using System.Reflection;
using BDTheque.GraphQL.Helpers;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

public class QueryableStringContainsHandler : QueryableStringOperationHandler
{
    public QueryableStringContainsHandler(InputParser inputParser) : base(inputParser)
    {
        CanBeNull = false;
    }

    protected override int Operation => DefaultFilterOperations.Contains;

    public override Expression HandleOperation(QueryableFilterContext context, IFilterOperationField field, IValueNode value, object? parsedValue)
    {
        if (parsedValue is null)
            throw new GraphQLException(ErrorHelper.CreateNonNullError(field, value, context));

        Expression property = context.GetInstance();

        if (property is MemberExpression memberExpression)
        {
            PropertyInfo? rawProperty = memberExpression.Member.ReflectedType?.GetProperties().FirstOrDefault(p => p.Name == memberExpression.Member.Name + "Raw");
            if (rawProperty is not null)
                property = Expression.Property(memberExpression.Expression, rawProperty);
        }

        return FilterExpressionBuilder.Contains(property, parsedValue);
    }
}
