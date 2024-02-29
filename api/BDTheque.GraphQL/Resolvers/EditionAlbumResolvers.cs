namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<EditionAlbum>]
public static class EditionAlbumResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Cote> GetCotes([Parent] EditionAlbum edition)
        => edition.Cotes.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Image> GetImages([Parent] EditionAlbum edition)
        => edition.Images.AsQueryable().AsNoTracking();
}
