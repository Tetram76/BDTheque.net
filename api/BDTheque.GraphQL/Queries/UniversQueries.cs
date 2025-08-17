namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class UniversQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Univers> GetUniversList(BDThequeContext dbContext)
        => dbContext.Univers;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Univers> GetUnivers(BDThequeContext dbContext)
        => dbContext.Univers;
}
