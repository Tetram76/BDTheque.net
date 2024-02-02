namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Types;

[QueryType]
public static class OptionQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<OptionType>]
    [UseSorting<OptionType>]
    [GraphQLType<ListType<OptionType>>]
    public static IQueryable<Option> GetOptions(BDThequeContext dbContext, CancellationToken cancellationToken)
        => dbContext.Options;
}
