namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class GenreLoaders
{
    [DataLoader]
    internal static async Task<Genre> GetGenreByIdAsync([ID] Guid id, IGenreRepository genreRepository, CancellationToken cancellationToken) =>
        await genreRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static Task<IQueryable<Genre>> GetGenreByNomAsync(string nom, BDThequeContext context) =>
        Task.FromResult(
            context.Genres
#pragma warning disable CA1862 // cannot use string.Contains as it is not supported for conversion to SQL statements
                .Where(genre => genre.Nom.ToLower().Contains(nom.ToLower()))
#pragma warning restore CA1862
                .AsQueryable()
        );

    [DataLoader]
    public static async Task<IQueryable<Album>> GetGenreAlbums(Genre genre, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(genre).Collection(e => e.GenresAlbums).LoadAsync(cancellationToken);
        return genre.GenresAlbums.Select(genreAlbum => genreAlbum.Album).AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Serie>> GetGenreSeries(Genre genre, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(genre).Collection(e => e.GenresSeries).LoadAsync(cancellationToken);
        return genre.GenresSeries.Select(genreSerie => genreSerie.Serie).AsQueryable();
    }
}
