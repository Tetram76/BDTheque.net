namespace BDTheque.GraphQL.DataLoaders;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class AlbumLoaders
{
    [DataLoader]
    internal static async Task<Album?> GetAlbumByIdAsync([ID(nameof(Album))] Guid id, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Albums.FirstOrDefaultAsync(album => album.Id == id, cancellationToken);

    [DataLoader]
    [SuppressMessage("Performance", "CA1862:Utiliser les surcharges de méthode «\u00a0StringComparison\u00a0» pour effectuer des comparaisons de chaînes sans respect de la casse")]
    internal static async Task<IReadOnlyList<Album>> GetAlbumByTitreAsync(string titre, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Albums
            .Where(album => album.Titre != null && album.Titre.ToLower().Contains(titre.ToLower()))
            .ToListAsync(cancellationToken);
}
