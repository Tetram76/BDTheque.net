namespace BDTheque.GraphQL.Handlers;

using System.Linq.Expressions;

using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

public class QueryableCharEqualsHandler(InputParser inputParser) : QueryableCharOperationHandler(inputParser)
{
    protected override int Operation => DefaultFilterOperations.Equals;

    public override Expression HandleOperation(QueryableFilterContext context, IFilterOperationField field, IValueNode value, object? parsedValue)
    {
        Expression property = context.GetInstance();
        return parsedValue is null || parsedValue.GetType() == property.Type
            ? Expression.Equal(property, Expression.Constant(parsedValue))
            : FilterExpressionBuilder.Equals(property, parsedValue);
    }
}
