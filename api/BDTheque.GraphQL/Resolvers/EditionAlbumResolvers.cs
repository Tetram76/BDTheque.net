namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<EditionAlbumType>]
public static class EditionAlbumResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<CoteEditionType>]
    [UseSorting<CoteEditionType>]
    [GraphQLType<ListType<CoteEditionType>>]
    public static IQueryable<CoteEdition> GetCotes([GraphQLType<EditionAlbumType>][Parent] EditionAlbum edition)
        => edition.CotesEditions.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering<ImageType>]
    [UseSorting<ImageType>]
    [GraphQLType<ListType<ImageType>>]
    public static IQueryable<Image> GetImages([GraphQLType<EditionAlbumType>][Parent] EditionAlbum edition)
        => edition.Images.AsQueryable();
}
