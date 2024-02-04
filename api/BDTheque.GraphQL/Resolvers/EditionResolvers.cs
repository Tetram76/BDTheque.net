namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [ExtendObjectType<EditionType>]
public class EditionResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditionAlbumType>>]
    public static IQueryable<EditionAlbum> GetAlbums([GraphQLType<EditionType>][Parent] Edition edition)
        => edition.EditionsAlbums.AsQueryable();
}
