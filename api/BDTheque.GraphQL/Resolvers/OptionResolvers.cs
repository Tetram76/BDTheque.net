namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Option>]
public static class OptionResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbums([Parent] Option option)
        => option.Albums.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSeries([Parent] Option option)
        => option.Series.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionFormatEditions([Parent] Option option)
        => option.EditionFormatEditions.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionOrientations([Parent] Option option)
        => option.EditionOrientations.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionReliures([Parent] Option option)
        => option.EditionReliures.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionSensLectures([Parent] Option option)
        => option.EditionSensLectures.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionTypeEditions([Parent] Option option)
        => option.EditionTypeEditions.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionsEtats([Parent] Option option)
        => option.EditionsEtats.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Image> GetImages([Parent] Option option)
        => option.Images.AsQueryable().AsNoTracking();
}
