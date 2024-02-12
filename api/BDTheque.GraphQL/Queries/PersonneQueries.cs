namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class PersonneQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Personne> GetPersonneList(BDThequeContext dbContext)
        => dbContext.Personnes;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Personne> GetPersonne(BDThequeContext dbContext)
        => dbContext.Personnes;

    public static Task<Personne> GetPersonneByIdAsync([ID] Guid id, IPersonneByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);

    public static Task<IReadOnlyList<Personne>> GetPersonneByNomAsync(string nom, IPersonneByNomDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(nom, cancellationToken);
}
