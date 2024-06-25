namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Collection>]
public static class CollectionResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<EditionAlbum> GetAlbums([Parent] Collection collection)
        => collection.EditionsAlbums.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSeries([Parent] Collection collection)
        => collection.Series.AsQueryable().AsNoTracking();
}
