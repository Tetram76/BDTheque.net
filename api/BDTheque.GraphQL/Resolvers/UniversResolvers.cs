namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Univers>]
public static class UniversResolvers
{
    public static async Task<IEnumerable<Guid>?> GetBranche([Parent] Univers univers, IUniversBrancheDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(univers, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetRacines([Parent] Univers univers, IUniversRacinesDataLoader loader, CancellationToken cancellationToken) =>
        univers.UniversRacines.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetParents([Parent] Univers univers, IUniversParentsDataLoader loader, CancellationToken cancellationToken) =>
        univers.UniversParents.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbums([Parent] Univers univers, IUniversAlbumsDataLoader loader, CancellationToken cancellationToken) =>
        univers.UniversAlbums.Select(genreAlbum => genreAlbum.Album).AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSeries([Parent] Univers univers, IUniversSeriesDataLoader loader, CancellationToken cancellationToken) =>
        univers.UniversSeries.Select(genreSerie => genreSerie.Serie).AsQueryable().AsNoTracking();
}
