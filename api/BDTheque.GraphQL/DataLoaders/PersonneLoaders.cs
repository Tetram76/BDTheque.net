namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;

public static class PersonneLoaders
{
    [DataLoader]
    internal static async Task<Personne?> GetPersonneByIdAsync([ID] Guid id, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Personnes.FirstOrDefaultAsync(personne => personne.Id == id, cancellationToken);

    [DataLoader]
    [SuppressMessage("Performance", "CA1862:Utiliser les surcharges de méthode «\u00a0StringComparison\u00a0» pour effectuer des comparaisons de chaînes sans respect de la casse")]
    internal static async Task<IReadOnlyList<Personne>> GetPersonneByNomAsync(string nom, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Personnes
            .Where(personne => personne.Nom.ToLower().Contains(nom.ToLower()))
            .ToListAsync(cancellationToken);
}
