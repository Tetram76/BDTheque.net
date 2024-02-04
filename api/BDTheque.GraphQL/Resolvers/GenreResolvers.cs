namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<GenreType>]
public static class GenreResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<AlbumType>]
    [UseSorting<AlbumType>]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums([GraphQLType<GenreType>][Parent] Genre genre)
        => genre.GenresAlbums.Select(genreAlbum => genreAlbum.Album).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<SerieType>]
    [UseSorting<SerieType>]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries([GraphQLType<GenreType>][Parent] Genre genre)
        => genre.GenresSeries.Select(genreSerie => genreSerie.Serie).AsQueryable();
}
