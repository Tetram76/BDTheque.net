namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class CollectionQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Collection> GetCollectionList(BDThequeContext dbContext) =>
        dbContext.Collections;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Collection> GetCollection(BDThequeContext dbContext) =>
        dbContext.Collections;
}
