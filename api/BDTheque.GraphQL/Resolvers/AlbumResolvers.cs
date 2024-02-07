namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Album>]
public static class AlbumResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Auteur> GetAuteurs([Parent] Album album)
        => album.AuteursAlbums.Select(auteurAlbum => auteurAlbum.Auteur).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<EditionAlbum> GetEditions([Parent] Album album)
        => album.EditionsAlbums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Genre> GetGenres([Parent] Album album)
        => album.GenresAlbums.Select(genreAlbum => genreAlbum.Genre).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetUnivers([Parent] Album album)
        => album.UniversAlbums.Select(universAlbum => universAlbum.Univers).AsQueryable();
}
