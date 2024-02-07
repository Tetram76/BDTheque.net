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
        => option.Albums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSeries([Parent] Option option)
        => option.Series.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionFormatEditions([Parent] Option option)
        => option.EditionFormatEditions.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionOrientations([Parent] Option option)
        => option.EditionOrientations.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionReliures([Parent] Option option)
        => option.EditionReliures.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionSensLectures([Parent] Option option)
        => option.EditionSensLectures.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Edition> GetEditionTypeEditions([Parent] Option option)
        => option.EditionTypeEditions.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<EditionAlbum> GetEditionsAlbums([Parent] Option option)
        => option.EditionsAlbums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Image> GetImages([Parent] Option option)
        => option.Images.AsQueryable();
}
