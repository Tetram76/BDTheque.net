namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Album>]
public static class AlbumResolvers
{
    public static async Task<Serie?> GetSerie([Parent] Album album, IAlbumSerieDataLoader loader, CancellationToken cancellationToken) =>
        album.Serie ??= await loader.LoadAsync(album, cancellationToken);

    public static async Task<Option?> GetNotation([Parent] Album album, IAlbumNotationDataLoader loader, CancellationToken cancellationToken) =>
        album.Notation ??= await loader.LoadAsync(album, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Auteur>> GetAuteurs([Parent] Album album, IAlbumAuteursDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(album, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Edition>> GetEditions([Parent] Album album, IAlbumEditionsDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(album, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Genre>> GetGenres([Parent] Album album, IAlbumGenresDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(album, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Univers>> GetUnivers([Parent] Album album, IAlbumUniversDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(album, cancellationToken);
}
