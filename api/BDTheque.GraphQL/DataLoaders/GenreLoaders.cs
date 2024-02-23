namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using Microsoft.EntityFrameworkCore;

public static class GenreLoaders
{
    [DataLoader]
    internal static async Task<Genre?> GetGenreByIdAsync([ID] Guid id, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Genres.FirstOrDefaultAsync(genre => genre.Id == id, cancellationToken);

    [DataLoader]
    [SuppressMessage("Performance", "CA1862:Utiliser les surcharges de méthode «\u00a0StringComparison\u00a0» pour effectuer des comparaisons de chaînes sans respect de la casse")]
    internal static async Task<IReadOnlyList<Genre>> GetGenreByNomAsync(string nom, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Genres
            .Where(genre => genre.Nom.ToLower().Contains(nom.ToLower()))
            .ToListAsync(cancellationToken);
}
