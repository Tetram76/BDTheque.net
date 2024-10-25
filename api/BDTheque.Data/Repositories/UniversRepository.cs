namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;

public class UniversRepository(BDThequeContext dbContext, IServiceProvider serviceProvider)
    : EditableEntityRepository<Univers>(dbContext, serviceProvider), IUniversRepository
{
    public async Task<bool> IsNameAllowed(Univers univers, CancellationToken cancellationToken = default) =>
        !await DbContext.Univers.AnyAsync(
            u => u.Id != univers.Id
                 && u.Nom.ToLower().Equals(univers.Nom.ToLower()),
            cancellationToken
        );
}
