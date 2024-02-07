namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Editeur>]
public static class EditeurResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Collection> GetCollections([Parent] Editeur editeur)
        => editeur.Collections.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSeries([Parent] Editeur editeur)
        => editeur.Series.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<EditionAlbum> GetEditions([Parent] Editeur editeur)
        => editeur.EditionsAlbums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbums([Parent] Editeur editeur)
        => editeur.EditionsAlbums.Select(edition => edition.Album).Distinct().AsQueryable();
}
