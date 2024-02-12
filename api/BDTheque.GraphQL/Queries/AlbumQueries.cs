namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class AlbumQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbumList(BDThequeContext dbContext)
        => dbContext.Albums;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Album> GetAlbum(BDThequeContext dbContext)
        => dbContext.Albums;

    public static Task<Album> GetAlbumByIdAsync([ID] Guid id, IAlbumByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);

    public static Task<IReadOnlyList<Album>> GetAlbumByTitreAsync(string titre, IAlbumByTitreDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(titre, cancellationToken);
}
