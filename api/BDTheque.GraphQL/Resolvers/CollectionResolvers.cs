namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Collection>]
public static class CollectionResolvers
{
    public static async Task<Editeur> GetEditeur([Parent] Collection collection, ICollectionEditeurDataLoader loader, CancellationToken cancellationToken) =>
        collection.Editeur ??= await loader.LoadAsync(collection, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Edition>> GetEditions([Parent] Collection collection, ICollectionEditionsDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(collection, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Serie>> GetSeries([Parent] Collection collection, ICollectionSeriesDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(collection, cancellationToken);
}
