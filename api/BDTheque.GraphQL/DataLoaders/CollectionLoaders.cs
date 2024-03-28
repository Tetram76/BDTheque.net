namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class CollectionLoaders
{
    [DataLoader]
    internal static async Task<Collection> GetCollectionByIdAsync([ID] Guid id, ICollectionRepository collectionRepository, CancellationToken cancellationToken)
        => await collectionRepository.GetById(id, cancellationToken);

    [DataLoader]
    [SuppressMessage("Performance", "CA1862:Utiliser les surcharges de méthode «\u00a0StringComparison\u00a0» pour effectuer des comparaisons de chaînes sans respect de la casse")]
    internal static async Task<IReadOnlyList<Collection>> GetCollectionByNomAsync(string nom, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Collections
            .Where(collection => collection.Nom.ToLower().Contains(nom.ToLower()))
            .ToListAsync(cancellationToken);
}
