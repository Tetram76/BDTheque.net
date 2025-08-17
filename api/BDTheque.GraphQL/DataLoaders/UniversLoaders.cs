namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class UniversLoaders
{
    [DataLoader]
    internal static async Task<Univers> GetUniversByIdAsync([ID] Guid id, IUniversRepository universRepository, CancellationToken cancellationToken) =>
        await universRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static Task<IQueryable<Univers>> GetUniversByNomAsync(string nom, BDThequeContext context) =>
        Task.FromResult(
            context.Univers
#pragma warning disable CA1862 // cannot use string.Contains as it is not supported for conversion to SQL statements
                .Where(univers => univers.Nom.ToLower().Contains(nom.ToLower()))
#pragma warning restore CA1862
                .AsQueryable()
        );

    [DataLoader]
    internal static async Task<IEnumerable<Guid>?> GetUniversBranche(Univers univers, BDThequeContext dbContext, CancellationToken cancellationToken) =>
        // await dbContext.Entry(univers).Reference(c => c.Branche).LoadAsync(cancellationToken);
        // return univers.Branche;
        null;

    [DataLoader]
    public static async Task<IQueryable<Univers>> GetUniversRacines(Univers univers, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(univers).Collection(u => u.UniversRacines).LoadAsync(cancellationToken);
        return univers.UniversRacines.AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Univers>> GetUniversParents(Univers univers, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(univers).Collection(u => u.UniversParents).LoadAsync(cancellationToken);
        return univers.UniversParents.AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Album>> GetUniversAlbums(Univers univers, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(univers).Collection(a => a.UniversAlbums).LoadAsync(cancellationToken);
        return univers.UniversAlbums.Select(universAlbum => universAlbum.Album).AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Serie>> GetUniversSeries(Univers univers, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(univers).Collection(a => a.UniversSeries).LoadAsync(cancellationToken);
        return univers.UniversSeries.Select(universAlbum => universAlbum.Serie).AsQueryable();
    }

}
