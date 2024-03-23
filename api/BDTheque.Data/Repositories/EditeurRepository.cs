namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;

public class EditeurRepository(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : EditableEntityRepository<Editeur>(serviceProvider, dbContext), IEditeurRepository
{
    public async Task<bool> IsNameAllowed(Editeur editeur, CancellationToken cancellationToken = default) =>
        !await DbContext.Editeurs.AnyAsync(
            g => g.Id != editeur.Id
                 && g.Nom.ToLower().Equals(editeur.Nom.ToLower()),
            cancellationToken
        );
}
