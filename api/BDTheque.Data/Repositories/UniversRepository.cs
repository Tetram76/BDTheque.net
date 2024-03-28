namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;

public class UniversRepository(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : EditableEntityRepository<Univers>(serviceProvider, dbContext), IUniversRepository
{
    public async Task<bool> IsNameAllowed(Univers univers, CancellationToken cancellationToken = default) =>
        !await DbContext.Univers.AnyAsync(
            u => u.Id != univers.Id
                 && u.Nom.ToLower().Equals(univers.Nom.ToLower()),
            cancellationToken
        );
}
