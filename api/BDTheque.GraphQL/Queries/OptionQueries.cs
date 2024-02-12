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
    public static IQueryable<Option> GetOptions(BDThequeContext dbContext)
        => dbContext.Options;

    public static Task<IReadOnlyList<Option>> GetOptionByCategoryAsync(OptionCategory category, IOptionByCategoryDataLoader dataLoader, CancellationToken cancellationToken)
        => dataLoader.LoadAsync(category, cancellationToken);

}
