namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.DataLoaders;
using BDTheque.GraphQL.Types;

[QueryType]
public static class AlbumQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<AlbumType>]
    [UseSorting<AlbumType>]
    [GraphQLType<ListType<AlbumType>>]
    public static IQueryable<Album> GetAlbums(BDThequeContext dbContext, CancellationToken cancellationToken)
        => dbContext.Albums;

    [GraphQLType<AlbumType>]
    public static Task<Album> GetAlbumById([ID] Guid id, IAlbumByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);
}
