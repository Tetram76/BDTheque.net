namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;

public static class EditionDetailLoaders
{
    [DataLoader]
    internal static async Task<Option?> GetEditionDetailEtat(EditionDetail editionDetail, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editionDetail).Reference(c => c.Etat).LoadAsync(cancellationToken);
        return editionDetail.Etat;
    }

    [DataLoader]
    internal static async Task<Option?> GetEditionDetailReliure(EditionDetail editionDetail, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editionDetail).Reference(c => c.Reliure).LoadAsync(cancellationToken);
        return editionDetail.Reliure;
    }

    [DataLoader]
    internal static async Task<Option?> GetEditionDetailFormatEdition(EditionDetail editionDetail, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editionDetail).Reference(c => c.FormatEdition).LoadAsync(cancellationToken);
        return editionDetail.FormatEdition;
    }

    [DataLoader]
    internal static async Task<Option?> GetEditionDetailTypeEdition(EditionDetail editionDetail, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editionDetail).Reference(c => c.TypeEdition).LoadAsync(cancellationToken);
        return editionDetail.TypeEdition;
    }

    [DataLoader]
    internal static async Task<Option?> GetEditionDetailOrientation(EditionDetail editionDetail, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editionDetail).Reference(c => c.Orientation).LoadAsync(cancellationToken);
        return editionDetail.Orientation;
    }

    [DataLoader]
    internal static async Task<Option?> GetEditionDetailSensLecture(EditionDetail editionDetail, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(editionDetail).Reference(c => c.SensLecture).LoadAsync(cancellationToken);
        return editionDetail.SensLecture;
    }
}
