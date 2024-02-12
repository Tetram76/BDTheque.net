namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class GenreQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Genre> GetGenreList(BDThequeContext dbContext)
        => dbContext.Genres;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Genre> GetGenre(BDThequeContext dbContext)
        => dbContext.Genres;

    public static Task<Genre> GetGenreByIdAsync([ID] Guid id, IGenreByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);

    public static Task<IReadOnlyList<Genre>> GetGenreByNomAsync(string nom, IGenreByNomDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(nom, cancellationToken);
}
