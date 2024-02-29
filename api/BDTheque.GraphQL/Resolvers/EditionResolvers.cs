namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Edition>]
public static class EditionResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<EditionAlbum> GetAlbums([Parent] Edition edition)
        => edition.EditionsAlbums.AsQueryable().AsNoTracking();
}
