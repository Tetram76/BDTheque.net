namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Types;

[QueryType]
public static class SerieQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<SerieType>]
    [UseSorting<SerieType>]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries(BDThequeContext dbContext, CancellationToken cancellationToken)
        => dbContext.Series;
}
