namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using Microsoft.EntityFrameworkCore;

public class EditeurLoaders
{
    [DataLoader]
    internal static async Task<Editeur?> GetEditeurByIdAsync([ID] Guid id, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Editeurs.FirstOrDefaultAsync(editeur => editeur.Id == id, cancellationToken);

    [DataLoader]
    [SuppressMessage("Performance", "CA1862:Utiliser les surcharges de méthode «\u00a0StringComparison\u00a0» pour effectuer des comparaisons de chaînes sans respect de la casse")]
    internal static async Task<IReadOnlyList<Editeur>> GetEditeurByNomAsync(string nom, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Editeurs
            .Where(editeur => editeur.Nom.ToLower().Contains(nom.ToLower()))
            .ToListAsync(cancellationToken);
}
