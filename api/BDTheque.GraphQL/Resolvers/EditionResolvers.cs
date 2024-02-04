namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<EditionType>]
public static class EditionResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<EditionAlbumType>]
    [UseSorting<EditionAlbumType>]
    [GraphQLType<ListType<EditionAlbumType>>]
    public static IQueryable<EditionAlbum> GetAlbums([GraphQLType<EditionType>][Parent] Edition edition)
        => edition.EditionsAlbums.AsQueryable();
}
