namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class CoteLoaders
{
    [DataLoader]
    internal static async Task<Cote> GetCoteByIdAsync([ID] Guid id, ICoteRepository coteRepository, CancellationToken cancellationToken) =>
        await coteRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static async Task<Edition> GetCoteEdition(Cote cote, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(cote).Reference(c => c.Edition).LoadAsync(cancellationToken);
        return cote.Edition;
    }
}
