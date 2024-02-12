namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using Microsoft.EntityFrameworkCore;

public class SerieLoaders
{
    [DataLoader]
    internal static async Task<Serie?> GetSerieByIdAsync([ID] Guid id, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Series.FirstOrDefaultAsync(serie => serie.Id == id, cancellationToken);

    [DataLoader]
    [SuppressMessage("Performance", "CA1862:Utiliser les surcharges de méthode «\u00a0StringComparison\u00a0» pour effectuer des comparaisons de chaînes sans respect de la casse")]
    internal static async Task<IReadOnlyList<Serie>> GetSerieByTitreAsync(string titre, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Series
            .Where(serie => serie.Titre != null && serie.Titre.ToLower().Contains(titre.ToLower()))
            .ToListAsync(cancellationToken);
}
