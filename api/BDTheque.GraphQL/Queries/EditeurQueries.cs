namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class EditeurQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Editeur> GetEditeurList(BDThequeContext dbContext)
        => dbContext.Editeurs;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Editeur> GetEditeur(BDThequeContext dbContext)
        => dbContext.Editeurs;
}
