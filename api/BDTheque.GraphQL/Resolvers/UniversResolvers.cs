namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [ExtendObjectType<UniversType>]
public class UniversResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<UniversType>>]
    public static IQueryable<Univers> GetRacines([GraphQLType<UniversType>][Parent] Univers univers)
        => univers.UniversRacines.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<UniversType>>]
    public static IQueryable<Univers> GetParents([GraphQLType<UniversType>][Parent] Univers univers)
        => univers.UniversParents.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums([GraphQLType<UniversType>][Parent] Univers univers)
        => univers.UniversAlbums.Select(genreAlbum => genreAlbum.Album).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries([GraphQLType<UniversType>][Parent] Univers univers)
        => univers.UniversSeries.Select(genreSerie => genreSerie.Serie).AsQueryable();
}
