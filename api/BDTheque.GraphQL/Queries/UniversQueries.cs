namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [QueryType]
public static class UniversQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<UniversType>>]
    public static IQueryable<Univers> GetUnivers(BDThequeContext dbContext)
        => dbContext.Univers;
}
