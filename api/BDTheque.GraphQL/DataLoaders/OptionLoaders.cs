namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Model.Enums;
using Microsoft.EntityFrameworkCore;

public static class OptionLoaders
{
    [DataLoader]
    internal static async Task<IReadOnlyList<Option>> GetOptionByCategoryAsync(OptionCategory category, BDThequeContext context, CancellationToken cancellationToken)
        => await context.Options
            .Where(option => option.Category == category)
            .OrderBy(option => option.Ordre)
            .ToListAsync(cancellationToken);
}
