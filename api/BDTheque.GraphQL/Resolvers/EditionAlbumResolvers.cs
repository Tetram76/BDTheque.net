namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [ExtendObjectType<EditionAlbumType>]
public class EditionAlbumResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<CoteAlbumType>>]
    public static IQueryable<CoteAlbum> GetCotes([GraphQLType<EditionAlbumType>][Parent] EditionAlbum edition)
        => edition.CotesAlbums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<CoteAlbumType>>]
    public static IQueryable<Image> GetImages([GraphQLType<EditionAlbumType>][Parent] EditionAlbum edition)
        => edition.Images.AsQueryable();
}
