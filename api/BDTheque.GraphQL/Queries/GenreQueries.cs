namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class GenreQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Genre> GetGenreList(BDThequeContext dbContext)
        => dbContext.Genres;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Genre> GetGenre(BDThequeContext dbContext)
        => dbContext.Genres;
}
