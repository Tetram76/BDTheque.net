namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class CollectionQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Collection> GetCollectionList(BDThequeContext dbContext)
        => dbContext.Collections;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Collection> GetCollection(BDThequeContext dbContext)
        => dbContext.Collections;

    public static Task<Collection> GetCollectionByIdAsync([ID] Guid id, ICollectionByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);

    public static Task<IReadOnlyList<Collection>> GetCollectionByNomAsync(string nom, ICollectionByNomDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(nom, cancellationToken);
}
