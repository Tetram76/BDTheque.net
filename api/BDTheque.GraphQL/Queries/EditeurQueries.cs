namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

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

    public static Task<Editeur> GetEditeurByIdAsync([ID] Guid id, IEditeurByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);

    public static Task<IReadOnlyList<Editeur>> GetEditeurByNomAsync(string nom, IEditeurByNomDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(nom, cancellationToken);
}
