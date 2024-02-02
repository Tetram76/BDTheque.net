namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Types;

[QueryType]
public static class CollectionQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<CollectionType>]
    [UseSorting<CollectionType>]
    [GraphQLType<ListType<CollectionType>>]
    public static IQueryable<Collection> GetCollections(BDThequeContext dbContext, CancellationToken cancellationToken)
        => dbContext.Collections;
}
