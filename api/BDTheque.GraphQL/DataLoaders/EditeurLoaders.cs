namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class EditeurLoaders
{
    [DataLoader]
    internal static async Task<Editeur> GetEditeurByIdAsync([ID] Guid id, IEditeurRepository editeurRepository, CancellationToken cancellationToken) =>
        await editeurRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static Task<IQueryable<Editeur>> GetEditeurByNomAsync(string nom, BDThequeContext context) =>
        Task.FromResult(
            context.Editeurs
#pragma warning disable CA1862 // cannot use string.Contains as it is not supported for conversion to SQL statements
                .Where(editeur => editeur.Nom.ToLower().Contains(nom.ToLower()))
#pragma warning restore CA1862
                .AsQueryable()
        );

    [DataLoader]
    public static async Task<IQueryable<Collection>> GetEditeurCollections(Editeur editeur, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editeur).Collection(e => e.Collections).LoadAsync(cancellationToken);
        return editeur.Collections.AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Serie>> GetEditeurSeries(Editeur editeur, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editeur).Collection(e => e.Series).LoadAsync(cancellationToken);
        return editeur.Series.AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Edition>> GetEditeurEditions(Editeur editeur, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editeur).Collection(e => e.Editions).LoadAsync(cancellationToken);
        return editeur.Editions.AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Album>> GetEditeurAlbums(Editeur editeur, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editeur).Collection(e => e.Editions).LoadAsync(cancellationToken);
        return editeur.Editions.Select(auteurAlbum => auteurAlbum.Album).Distinct().AsQueryable();
    }

}
