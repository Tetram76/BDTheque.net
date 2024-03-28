namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;

public class PersonneRepository(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : EditableEntityRepository<Personne>(serviceProvider, dbContext), IPersonneRepository
{
    public async Task<bool> IsNameAllowed(Personne personne, CancellationToken cancellationToken = default) =>
        !await DbContext.Personnes.AnyAsync(
            p => p.Id != personne.Id
                 && p.Nom.ToLower().Equals(personne.Nom.ToLower()),
            cancellationToken
        );
}
