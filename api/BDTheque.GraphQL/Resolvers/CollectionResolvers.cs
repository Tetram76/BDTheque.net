namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<CollectionType>]
public static class CollectionResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<EditionAlbumType>]
    [UseSorting<EditionAlbumType>]
    [GraphQLType<ListType<EditionAlbumType>>]
    public static IQueryable<EditionAlbum> GetAlbums([GraphQLType<CollectionType>][Parent] Collection collection)
        => collection.EditionsAlbums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<SerieType>]
    [UseSorting<SerieType>]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries([GraphQLType<CollectionType>][Parent] Collection collection)
        => collection.Series.AsQueryable();
}
