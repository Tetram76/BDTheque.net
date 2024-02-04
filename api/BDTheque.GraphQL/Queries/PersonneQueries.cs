namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class PersonneQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<PersonneType>]
    [UseSorting<PersonneType>]
    [GraphQLType<ListType<PersonneType>>]
    public static IQueryable<Personne> GetPersonnes(BDThequeContext dbContext)
        => dbContext.Personnes;
}
