namespace BDTheque.GraphQL.Handlers;

using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

public class QueryableCharNotEqualsHandler(InputParser inputParser) : QueryableCharOperationHandler(inputParser)
{
    protected override int Operation => DefaultFilterOperations.NotEquals;

    public override Expression HandleOperation(QueryableFilterContext context, IFilterOperationField field, IValueNode value, object? parsedValue)
    {
        Expression property = context.GetInstance();
        return parsedValue is null || parsedValue.GetType() == property.Type
            ? Expression.NotEqual(property, Expression.Constant(parsedValue))
            : FilterExpressionBuilder.NotEquals(property, parsedValue);
    }
}
