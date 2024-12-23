namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class AlbumLoaders
{
    [DataLoader]
    internal static async Task<Album> GetAlbumByIdAsync([ID] Guid id, IAlbumRepository albumRepository, CancellationToken cancellationToken) =>
        await albumRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static Task<IQueryable<Album>> GetAlbumByTitreAsync(string titre, BDThequeContext context) =>
        Task.FromResult(
            context.Albums
                .Where(
                    album => album.Titre != null
#pragma warning disable CA1862 // cannot use string.Contains as it is not supported for conversion to SQL statements
                             && album.Titre.ToLower().Contains(titre.ToLower())
#pragma warning restore CA1862
                )
                .AsQueryable()
        );

    [DataLoader]
    internal static async Task<Serie?> GetAlbumSerie(Album album, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(album).Reference(c => c.Serie).LoadAsync(cancellationToken);
        return album.Serie;
    }

    [DataLoader]
    internal static async Task<Option?> GetAlbumNotation(Album album, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(album).Reference(c => c.Notation).LoadAsync(cancellationToken);
        return album.Notation;
    }

    [DataLoader]
    public static async Task<IQueryable<Auteur>> GetAlbumAuteurs(Album album, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(album).Collection(a => a.AuteursAlbums).LoadAsync(cancellationToken);
        return album.AuteursAlbums.Select(auteurAlbum => auteurAlbum.Auteur).AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Edition>> GetAlbumEditions(Album album, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(album).Collection(a => a.Editions).LoadAsync(cancellationToken);
        return album.Editions.AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Genre>> GetAlbumGenres(Album album, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(album).Collection(a => a.GenresAlbums).LoadAsync(cancellationToken);
        return album.GenresAlbums.Select(genreAlbum => genreAlbum.Genre).AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Univers>> GetAlbumUnivers(Album album, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(album).Collection(a => a.UniversAlbums).LoadAsync(cancellationToken);
        return album.UniversAlbums.Select(universAlbum => universAlbum.Univers).AsQueryable();
    }
}
