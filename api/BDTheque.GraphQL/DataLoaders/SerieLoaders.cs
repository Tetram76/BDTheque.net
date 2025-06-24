namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class SerieLoaders
{
    [DataLoader]
    internal static async Task<Serie> GetSerieByIdAsync([ID] Guid id, ISerieRepository serieRepository, CancellationToken cancellationToken) =>
        await serieRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static Task<IQueryable<Serie>> GetSerieByTitreAsync(string titre, BDThequeContext context) =>
        Task.FromResult(
            context.Series
#pragma warning disable CA1862 // cannot use string.Contains as it is not supported for conversion to SQL statements
                .Where(serie => serie.Titre != null && serie.Titre.ToLower().Contains(titre.ToLower()))
#pragma warning restore CA1862
                .AsQueryable()
        );

    [DataLoader]
    internal static async Task<Editeur?> GetSerieEditeur(Serie serie, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(serie).Reference(s => s.Editeur).LoadAsync(cancellationToken);
        return serie.Editeur;
    }

    [DataLoader]
    internal static async Task<Collection?> GetSerieCollection(Serie serie, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(serie).Reference(s => s.Collection).LoadAsync(cancellationToken);
        return serie.Collection;
    }

    [DataLoader]
    internal static async Task<EditionDetail> GetSerieModele(Serie serie, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(serie).Reference(s => s.ModeleEdition).LoadAsync(cancellationToken);
        return serie.ModeleEdition;
    }

    [DataLoader]
    internal static async Task<Option?> GetSerieNotation(Serie serie, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(serie).Reference(s => s.Notation).LoadAsync(cancellationToken);
        return serie.Notation;
    }

}
