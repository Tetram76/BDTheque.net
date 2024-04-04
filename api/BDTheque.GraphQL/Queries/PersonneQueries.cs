namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

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
}
