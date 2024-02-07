namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Auteur>]
public static class AuteurResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbums([Parent] Auteur auteur)
        => auteur.AuteursAlbums.Select(auteurAlbum => auteurAlbum.Album).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSeries([Parent] Auteur auteur)
        => auteur.AuteursSeries.Select(auteurSerie => auteurSerie.Serie).AsQueryable();
}
