namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Model.Enums;

public static class OptionLoaders
{
    [DataLoader]
    internal static Task<IQueryable<Option>> GetOptionListByCategoryAsync(OptionCategory category, BDThequeContext context) =>
        Task.FromResult(
            context.Options
                .Where(option => option.Category == category)
                .OrderBy(option => option.Ordre)
                .AsQueryable()
        );
}
