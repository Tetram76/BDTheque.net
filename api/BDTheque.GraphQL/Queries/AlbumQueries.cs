namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [QueryType]
public static class AlbumQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums(BDThequeContext dbContext, CancellationToken cancellationToken)
        => dbContext.Albums;

    [GraphQLType<AlbumType>]
    public static Task<Album> GetAlbumById([ID] Guid id, IAlbumByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);
}
