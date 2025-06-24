namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Editeur>]
public static class EditeurResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Collection>> GetCollections([Parent] Editeur editeur, IEditeurCollectionsDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editeur, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Serie>> GetSeries([Parent] Editeur editeur, IEditeurSeriesDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editeur, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Edition>> GetEditions([Parent] Editeur editeur, IEditeurEditionsDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editeur, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Album>> GetAlbums([Parent] Editeur editeur, IEditeurAlbumsDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editeur, cancellationToken);
}
