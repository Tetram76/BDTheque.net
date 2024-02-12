namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class SerieQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSerieList(BDThequeContext dbContext)
        => dbContext.Series;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Serie> GetSerie(BDThequeContext dbContext)
        => dbContext.Series;

    public static Task<Serie> GetSerieByIdAsync([ID] Guid id, ISerieByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);

    public static Task<IReadOnlyList<Serie>> GetSerieByTitreAsync(string titre, ISerieByTitreDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(titre, cancellationToken);
}
