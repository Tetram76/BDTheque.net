namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.GraphQL.DataLoaders;
using BDTheque.Model.Enums;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[QueryType]
public static class OptionQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Option> GetOptionList(BDThequeContext dbContext)
        => dbContext.Options;

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    public static IQueryable<Option> GetOption(BDThequeContext dbContext)
        => dbContext.Options;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static Task<IQueryable<Option>> GetOptionListByCategoryAsync(OptionCategory category, IOptionListByCategoryDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(category, cancellationToken);
}
