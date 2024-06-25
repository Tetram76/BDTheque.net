namespace BDTheque.GraphQL.Handlers;

using BDTheque.GraphQL.Filters;

using HotChocolate.Configuration;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;

public abstract class QueryableCharOperationHandler(InputParser inputParser) : QueryableOperationHandlerBase(inputParser)
{
    protected abstract int Operation { get; }

    public override bool CanHandle(ITypeCompletionContext context, IFilterInputTypeDefinition typeDefinition, IFilterFieldDefinition fieldDefinition) =>
        context.Type is CharOperationFilterInputType &&
        fieldDefinition is FilterOperationFieldDefinition operationField &&
        operationField.Id == Operation;
}
