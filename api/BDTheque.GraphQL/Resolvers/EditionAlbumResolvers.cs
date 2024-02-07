namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<EditionAlbum>]
public static class EditionAlbumResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<CoteEdition> GetCotes([Parent] EditionAlbum edition)
        => edition.CotesEditions.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Image> GetImages([Parent] EditionAlbum edition)
        => edition.Images.AsQueryable();
}
