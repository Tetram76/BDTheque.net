namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;

using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class UniversMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Univers> CreateUnivers(UniversCreateInput univers, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Univers.AnyAsync(g => g.Nom == univers.Nom, cancellationToken))
            throw new AlreadyExistsException();

        Univers newUnivers = univers.BuildEntity<Univers>();
        dbContext.Univers.Add(newUnivers);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(UniversSubscriptions.UniversCreated), newUnivers, cancellationToken);
        return newUnivers;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Univers> UpdateUnivers(UniversUpdateInput univers, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Univers? oldUnivers = await dbContext.Univers.Where(p => p.Id == univers.Id).SingleOrDefaultAsync(cancellationToken);
        if (oldUnivers is null)
            throw new NotFoundIdException();
        if (univers.Nom.HasValue && await dbContext.Univers.AnyAsync(g => g.Id != oldUnivers.Id && g.Nom == univers.Nom, cancellationToken))
            throw new AlreadyExistsException();

        univers.ApplyUpdate(oldUnivers);
        dbContext.Update(oldUnivers);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(UniversSubscriptions.UniversUpdated), oldUnivers, cancellationToken);
        return oldUnivers;
    }

    [Error<NotFoundIdException>]
    public static async Task<Univers> DeleteUnivers([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Univers? univers = await dbContext.Univers.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (univers is null)
            throw new NotFoundIdException();

        dbContext.Univers.Remove(univers);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(UniversSubscriptions.UniversDeleted), univers, cancellationToken);
        return univers;
    }
}
