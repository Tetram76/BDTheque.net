namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [ExtendObjectType<OptionType>]
public class OptionResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums([GraphQLType<OptionType>][Parent] Option option)
        => option.Albums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<SerieType>>]
    public static IQueryable<Serie> GetSeries([GraphQLType<OptionType>][Parent] Option option)
        => option.Series.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditionType>>]
    public static IQueryable<Edition> GetEditionFormatEditions([GraphQLType<OptionType>][Parent] Option option)
        => option.EditionFormatEditions.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditionType>>]
    public static IQueryable<Edition> GetEditionOrientations([GraphQLType<OptionType>][Parent] Option option)
        => option.EditionOrientations.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditionType>>]
    public static IQueryable<Edition> GetEditionReliures([GraphQLType<OptionType>][Parent] Option option)
        => option.EditionReliures.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditionType>>]
    public static IQueryable<Edition> GetEditionSensLectures([GraphQLType<OptionType>][Parent] Option option)
        => option.EditionSensLectures.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditionType>>]
    public static IQueryable<Edition> GetEditionTypeEditions([GraphQLType<OptionType>][Parent] Option option)
        => option.EditionTypeEditions.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditionAlbumType>>]
    public static IQueryable<EditionAlbum> GetEditionsAlbums([GraphQLType<OptionType>][Parent] Option option)
        => option.EditionsAlbums.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<ImageType>>]
    public static IQueryable<Image> GetImages([GraphQLType<OptionType>][Parent] Option option)
        => option.Images.AsQueryable();
}
