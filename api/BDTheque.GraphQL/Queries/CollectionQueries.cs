namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [QueryType]
public static class CollectionQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<CollectionType>>]
    public static IQueryable<Collection> GetCollections(BDThequeContext dbContext)
        => dbContext.Collections;
}
