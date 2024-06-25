namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;

public static class UniversLoaders
{
    [DataLoader]
    internal static async Task<Univers?> GetUniversByIdAsync([ID] Guid id, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Univers.FirstOrDefaultAsync(univers => univers.Id == id, cancellationToken);

    [DataLoader]
    [SuppressMessage("Performance", "CA1862:Utiliser les surcharges de méthode «\u00a0StringComparison\u00a0» pour effectuer des comparaisons de chaînes sans respect de la casse")]
    internal static async Task<IReadOnlyList<Univers>> GetUniversByNomAsync(string nom, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Univers
            .Where(univers => univers.Nom.ToLower().Contains(nom.ToLower()))
            .ToListAsync(cancellationToken);
}
