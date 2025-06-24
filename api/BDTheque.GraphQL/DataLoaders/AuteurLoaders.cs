namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;

public static class AuteurLoaders
{
    [DataLoader]
    public static async Task<IQueryable<Album>> GetAuteurAlbums(Auteur auteur, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(auteur).Collection(a => a.AuteursAlbums).LoadAsync(cancellationToken);
        return auteur.AuteursAlbums.Select(auteurAlbum => auteurAlbum.Album).AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Serie>> GetAuteurSeries(Auteur auteur, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(auteur).Collection(a => a.AuteursSeries).LoadAsync(cancellationToken);
        return auteur.AuteursSeries.Select(auteurAlbum => auteurAlbum.Serie).AsQueryable();
    }
}
