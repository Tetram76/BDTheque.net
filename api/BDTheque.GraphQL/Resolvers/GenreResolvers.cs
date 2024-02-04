namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [ExtendObjectType<GenreType>]
public class GenreResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums([GraphQLType<GenreType>][Parent] Genre genre)
        => genre.GenresAlbums.Select(genreAlbum => genreAlbum.Album).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries([GraphQLType<GenreType>][Parent] Genre genre)
        => genre.GenresSeries.Select(genreSerie => genreSerie.Serie).AsQueryable();
}
