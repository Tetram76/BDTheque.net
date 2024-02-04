namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [ExtendObjectType<AlbumType>]
public static class AlbumResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<AuteurType>>]
    public static IQueryable<Auteur> GetAuteurs([GraphQLType<AlbumType>][Parent] Album album)
        => album.AuteursAlbums.Select(auteurAlbum => auteurAlbum.Auteur).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditionAlbumType>>]
    public static IQueryable<EditionAlbum> GetEditions([GraphQLType<AlbumType>][Parent] Album album)
        => album.EditionsAlbums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<GenreType>>]
    public static IQueryable<Genre> GetGenres([GraphQLType<AlbumType>][Parent] Album album)
        => album.GenresAlbums.Select(genreAlbum => genreAlbum.Genre).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<UniversType>>]
    public static IQueryable<Univers> GetUnivers([GraphQLType<AlbumType>][Parent] Album album)
        => album.UniversAlbums.Select(universAlbum => universAlbum.Univers).AsQueryable();
}
