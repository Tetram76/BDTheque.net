namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Types;

[QueryType]
public static class PersonneQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<PersonneType>]
    [UseSorting<PersonneType>]
    [GraphQLType<ListType<PersonneType>>]
    public static IQueryable<Personne> GetPersonnes(BDThequeContext dbContext, CancellationToken cancellationToken)
        => dbContext.Personnes;
}
