namespace BDTheque.GraphQL.Resolvers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Serie>]
public static class SerieResolvers
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbums([Parent] Serie serie)
        => serie.Albums.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Auteur> GetAuteurs([Parent] Serie serie)
        => serie.AuteursSeries.Select(auteurSerie => auteurSerie.Auteur).AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Genre> GetGenres([Parent] Serie serie)
        => serie.GenresSeries.Select(auteurSerie => auteurSerie.Genre).AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetUnivers([Parent] Serie serie)
        => serie.UniversSeries.Select(universSerie => universSerie.Univers).AsQueryable().AsNoTracking();
}
