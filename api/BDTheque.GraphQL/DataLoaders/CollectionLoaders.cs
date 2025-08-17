namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class CollectionLoaders
{
    [DataLoader]
    internal static async Task<Collection> GetCollectionByIdAsync([ID] Guid id, ICollectionRepository collectionRepository, CancellationToken cancellationToken) =>
        await collectionRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static Task<IQueryable<Collection>> GetCollectionByNomAsync(string nom, BDThequeContext context) =>
        Task.FromResult(
            context.Collections
#pragma warning disable CA1862 // cannot use string.Contains as it is not supported for conversion to SQL statements
                .Where(collection => collection.Nom.ToLower().Contains(nom.ToLower()))
#pragma warning restore CA1862
                .AsQueryable()
        );

    [DataLoader]
    internal static async Task<Editeur> GetCollectionEditeur(Collection collection, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(collection).Reference(c => c.Editeur).LoadAsync(cancellationToken);
        return collection.Editeur;
    }

    [DataLoader]
    internal static async Task<IQueryable<Edition>> GetCollectionEditions(Collection collection, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(collection).Collection(c => c.Editions).LoadAsync(cancellationToken);
        return collection.Editions.AsQueryable();
    }

    [DataLoader]
    internal static async Task<IQueryable<Serie>> GetCollectionSeries(Collection collection, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(collection).Collection(c => c.Series).LoadAsync(cancellationToken);
        return collection.Series.AsQueryable();
    }
}
