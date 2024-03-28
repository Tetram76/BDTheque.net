namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;

public class CollectionRepository(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : EditableEntityRepository<Collection>(serviceProvider, dbContext), ICollectionRepository
{
    public async Task<bool> IsNameAllowed(Collection collection, CancellationToken cancellationToken = default) =>
        !await DbContext.Collections.AnyAsync(
            c => c.EditeurId == collection.EditeurId
                 && c.Id != collection.Id
                 && c.Nom.ToLower().Equals(collection.Nom.ToLower()),
            cancellationToken
        );
}
