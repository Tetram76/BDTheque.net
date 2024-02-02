namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Types;

[QueryType]
public static class UniversQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<UniversType>]
    [UseSorting<UniversType>]
    [GraphQLType<ListType<UniversType>>]
    public static IQueryable<Univers> GetUnivers(BDThequeContext dbContext, CancellationToken cancellationToken)
        => dbContext.Univers;
}
