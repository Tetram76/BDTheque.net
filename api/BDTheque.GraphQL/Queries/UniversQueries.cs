namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class UniversQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetUniversList(BDThequeContext dbContext)
        => dbContext.Univers;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Univers> GetUnivers(BDThequeContext dbContext)
        => dbContext.Univers;

    public static Task<Univers> GetUniversByIdAsync([ID] Guid id, IUniversByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);

    public static Task<IReadOnlyList<Univers>> GetUniversByNomAsync(string nom, IUniversByNomDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(nom, cancellationToken);
}
