namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class EditionLoaders
{
    [DataLoader]
    internal static async Task<Edition> GetEditionByIdAsync([ID] Guid id, IEditionRepository editionRepository, CancellationToken cancellationToken) =>
        await editionRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static async Task<EditionDetail> GetEditionDetail(Edition edition, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(edition).Reference(c => c.Detail).LoadAsync(cancellationToken);
        return edition.Detail;
    }

    [DataLoader]
    internal static async Task<Album> GetEditionAlbum(Edition edition, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(edition).Reference(c => c.Album).LoadAsync(cancellationToken);
        return edition.Album;
    }

    [DataLoader]
    internal static async Task<Editeur> GetEditionEditeur(Edition edition, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(edition).Reference(c => c.Editeur).LoadAsync(cancellationToken);
        return edition.Editeur;
    }

    [DataLoader]
    internal static async Task<Collection?> GetEditionCollection(Edition edition, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(edition).Reference(c => c.Collection).LoadAsync(cancellationToken);
        return edition.Collection;
    }

    [DataLoader]
    public static async Task<IQueryable<Cote>> GetEditionCotes(Edition edition, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(edition).Collection(e => e.Cotes).LoadAsync(cancellationToken);
        return edition.Cotes.AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Image>> GetEditionImages(Edition edition, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(edition).Collection(e => e.Images).LoadAsync(cancellationToken);
        return edition.Images.AsQueryable();
    }
}
