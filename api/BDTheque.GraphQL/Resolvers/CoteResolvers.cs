namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<Cote>]
public static class CoteResolvers
{
    public static async Task<Edition> GetEdition([Parent] Cote cote, ICoteEditionDataLoader loader, CancellationToken cancellationToken) =>
        cote.Edition ??= await loader.LoadAsync(cote, cancellationToken);
}
