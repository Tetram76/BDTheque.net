namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

[QueryType]
public static class GenreQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<GenreType>>]
    public static IQueryable<Genre> GetGenres(BDThequeContext dbContext)
        => dbContext.Genres;

    [GraphQLType<GenreType>]
    public static Task<Genre> GetGenreById([ID] Guid id, IGenreByIdDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(id, cancellationToken);

    [GraphQLType<ListType<GenreType>>]
    public static Task<IReadOnlyList<Genre>> GetGenreByNom(string nom, IGenreByNomDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(nom, cancellationToken);
}
