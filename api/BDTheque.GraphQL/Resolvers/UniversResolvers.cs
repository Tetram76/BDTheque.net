namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Univers>]
public static class UniversResolvers
{
    public static IEnumerable<Guid>? GetBranche([Parent] Univers univers)
        => univers.UniversBranches;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetRacines([Parent] Univers univers)
        => univers.UniversRacines.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetParents([Parent] Univers univers)
        => univers.UniversParents.AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbums([Parent] Univers univers)
        => univers.UniversAlbums.Select(genreAlbum => genreAlbum.Album).AsQueryable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Serie> GetSeries([Parent] Univers univers)
        => univers.UniversSeries.Select(genreSerie => genreSerie.Serie).AsQueryable();
}
