namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [QueryType]
public static class OptionQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<OptionType>>]
    public static IQueryable<Option> GetOptions(BDThequeContext dbContext)
        => dbContext.Options;
}
