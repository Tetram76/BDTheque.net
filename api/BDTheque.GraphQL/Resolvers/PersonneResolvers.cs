namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<PersonneType>]
public static class PersonneResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<AlbumType>]
    [UseSorting<AlbumType>]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums([GraphQLType<PersonneType>][Parent] Personne personne)
        => personne.Auteurs.SelectMany(auteur => auteur.AuteursAlbums).Select(auteurAlbum => auteurAlbum.Album).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<SerieType>]
    [UseSorting<SerieType>]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries([GraphQLType<PersonneType>][Parent] Personne personne)
        => personne.Auteurs.SelectMany(auteur => auteur.AuteursSeries).Select(auteurSerie => auteurSerie.Serie).AsQueryable();
}
