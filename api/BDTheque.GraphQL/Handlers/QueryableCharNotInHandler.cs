namespace BDTheque.GraphQL.Handlers;

using System.Linq.Expressions;

using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

public class QueryableCharNotInHandler(InputParser inputParser) : QueryableCharOperationHandler(inputParser)
{
    protected override int Operation => DefaultFilterOperations.NotIn;

    public override Expression HandleOperation(QueryableFilterContext context, IFilterOperationField field, IValueNode value, object? parsedValue)
    {
        Expression property = context.GetInstance();

        return
            FilterExpressionBuilder.Not(FilterExpressionBuilder.In(property, context.RuntimeTypes.Peek().Source, parsedValue));
    }
}
