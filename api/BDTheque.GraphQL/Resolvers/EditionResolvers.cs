namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Edition>]
public static class EditionResolvers
{
    public static async Task<EditionDetail> GetDetail([Parent] Edition edition, IEditionDetailDataLoader loader, CancellationToken cancellationToken) =>
        edition.Detail ??= await loader.LoadAsync(edition, cancellationToken);

    public static async Task<Album> GetAlbum([Parent] Edition edition, IEditionAlbumDataLoader loader, CancellationToken cancellationToken) =>
        edition.Album ??= await loader.LoadAsync(edition, cancellationToken);

    public static async Task<Editeur> GetEditeur([Parent] Edition edition, IEditionEditeurDataLoader loader, CancellationToken cancellationToken) =>
        edition.Editeur ??= await loader.LoadAsync(edition, cancellationToken);

    public static async Task<Collection?> GetCollection([Parent] Edition edition, IEditionCollectionDataLoader loader, CancellationToken cancellationToken) =>
        edition.Collection ??= await loader.LoadAsync(edition, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Cote>> GetCotes([Parent] Edition edition, IEditionCotesDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(edition, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Image>> GetImages([Parent] Edition edition, IEditionImagesDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(edition, cancellationToken);
}
