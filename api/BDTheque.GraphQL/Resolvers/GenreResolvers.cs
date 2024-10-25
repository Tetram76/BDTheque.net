namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Genre>]
public static class GenreResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Album>> GetAlbums([Parent] Genre genre, IGenreAlbumsDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(genre, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Serie>> GetSeries([Parent] Genre genre, IGenreSeriesDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(genre, cancellationToken);
}
