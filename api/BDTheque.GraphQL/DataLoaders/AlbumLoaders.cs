namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories;

public static class AlbumLoaders
{
    [DataLoader]
    internal static async Task<Album> GetAlbumByIdAsync([ID] Guid id, IAlbumRepository albumRepository, CancellationToken cancellationToken)
        => await albumRepository.GetById(id, cancellationToken);

    [DataLoader]
    [SuppressMessage("Performance", "CA1862:Utiliser les surcharges de méthode «\u00a0StringComparison\u00a0» pour effectuer des comparaisons de chaînes sans respect de la casse")]
    internal static async Task<IReadOnlyList<Album>> GetAlbumByTitreAsync(string titre, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Albums
            .Where(album => album.Titre != null && album.Titre.ToLower().Contains(titre.ToLower()))
            .ToListAsync(cancellationToken);
}
