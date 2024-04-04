namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Auteur>]
public static class AuteurResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Album>> GetAlbums([Parent] Auteur auteur, IAuteurAlbumsDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(auteur, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Serie>> GetSeries([Parent] Auteur auteur, IAuteurSeriesDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(auteur, cancellationToken);
}
