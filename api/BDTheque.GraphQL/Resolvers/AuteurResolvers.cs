namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<AuteurType>]
public static class AuteurResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<AlbumType>]
    [UseSorting<AlbumType>]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums([GraphQLType<AuteurType>][Parent] Auteur auteur)
        => auteur.AuteursAlbums.Select(auteurAlbum => auteurAlbum.Album).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<SerieType>]
    [UseSorting<SerieType>]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries([GraphQLType<AuteurType>][Parent] Auteur auteur)
        => auteur.AuteursSeries.Select(auteurSerie => auteurSerie.Serie).AsQueryable();
}
