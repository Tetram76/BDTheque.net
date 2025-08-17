namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Personne>]
public static class PersonneResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Album>> GetAlbums([Parent] Personne personne, IPersonneAlbumsDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(personne, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Serie>> GetSeries([Parent] Personne personne, IPersonneSeriesDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(personne, cancellationToken);
}
