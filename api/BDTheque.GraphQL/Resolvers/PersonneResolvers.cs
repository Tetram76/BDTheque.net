namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Personne>]
public static class PersonneResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbums([Parent] Personne personne)
        => personne.Auteurs.SelectMany(auteur => auteur.AuteursAlbums).Select(auteurAlbum => auteurAlbum.Album).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSeries([Parent] Personne personne)
        => personne.Auteurs.SelectMany(auteur => auteur.AuteursSeries).Select(auteurSerie => auteurSerie.Serie).AsQueryable();
}
