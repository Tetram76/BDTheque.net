namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<SerieType>]
public static class SerieResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<AlbumType>]
    [UseSorting<AlbumType>]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums([GraphQLType<SerieType>][Parent] Serie serie)
        => serie.Albums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<AuteurType>]
    [UseSorting<AuteurType>]
    [GraphQLType<ListType<AuteurType>>]
    public static IQueryable<Auteur> GetAuteurs([GraphQLType<SerieType>][Parent] Serie serie)
        => serie.AuteursSeries.Select(auteurSerie => auteurSerie.Auteur).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<GenreType>]
    [UseSorting<GenreType>]
    [GraphQLType<ListType<GenreType>>]
    public static IQueryable<Genre> GetGenres([GraphQLType<SerieType>][Parent] Serie serie)
        => serie.GenresSeries.Select(auteurSerie => auteurSerie.Genre).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<UniversType>]
    [UseSorting<UniversType>]
    [GraphQLType<ListType<UniversType>>]
    public static IQueryable<Univers> GetUnivers([GraphQLType<SerieType>][Parent] Serie serie)
        => serie.UniversSeries.Select(universSerie => universSerie.Univers).AsQueryable();
}
