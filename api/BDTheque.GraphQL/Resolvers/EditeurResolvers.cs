namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<EditeurType>]
public static class EditeurResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<CollectionType>]
    [UseSorting<CollectionType>]
    [GraphQLType<ListType<CollectionType>>]
    public static IQueryable<Collection> GetCollections([GraphQLType<EditeurType>][Parent] Editeur editeur)
        => editeur.Collections.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<SerieType>]
    [UseSorting<SerieType>]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries([GraphQLType<EditeurType>][Parent] Editeur editeur)
        => editeur.Series.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<EditionAlbumType>]
    [UseSorting<EditionAlbumType>]
    [GraphQLType<ListType<EditionAlbumType>>]
    public static IQueryable<EditionAlbum> GetEditions([GraphQLType<EditeurType>][Parent] Editeur editeur)
        => editeur.EditionsAlbums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<AlbumType>]
    [UseSorting<AlbumType>]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums([GraphQLType<EditeurType>][Parent] Editeur editeur)
        => editeur.EditionsAlbums.Select(edition => edition.Album).Distinct().AsQueryable();
}
