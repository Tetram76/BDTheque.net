namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Serie>]
public static class SerieResolvers
{
    public static async Task<Editeur?> GetEditeur([Parent] Serie serie, ISerieEditeurDataLoader loader, CancellationToken cancellationToken) =>
        serie.Editeur ??= await loader.LoadAsync(serie, cancellationToken);

    public static async Task<Collection?> GetCollection([Parent] Serie serie, ISerieCollectionDataLoader loader, CancellationToken cancellationToken) =>
        serie.Collection ??= await loader.LoadAsync(serie, cancellationToken);

    public static async Task<EditionDetail> GetModeleEdition([Parent] Serie serie, ISerieModeleDataLoader loader, CancellationToken cancellationToken) =>
        serie.ModeleEdition ??= await loader.LoadAsync(serie, cancellationToken);

    public static async Task<Option?> GetNotation([Parent] Serie serie, ISerieNotationDataLoader loader, CancellationToken cancellationToken) =>
        serie.Notation ??= await loader.LoadAsync(serie, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Album> GetAlbums([Parent] Serie serie) =>
        serie.Albums.AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Auteur> GetAuteurs([Parent] Serie serie) =>
        serie.AuteursSeries.Select(auteurSerie => auteurSerie.Auteur).AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Genre> GetGenres([Parent] Serie serie) =>
        serie.GenresSeries.Select(auteurSerie => auteurSerie.Genre).AsQueryable().AsNoTracking();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetUnivers([Parent] Serie serie) =>
        serie.UniversSeries.Select(universSerie => universSerie.Univers).AsQueryable().AsNoTracking();
}
