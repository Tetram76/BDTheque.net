namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Univers>]
public static partial class UniversMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Univers> CreateUnivers(UniversCreateInput univers, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Univers.AnyAsync(g => g.Nom == univers.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Univers name \"{univers.Nom.Value}\" is already used");

        Univers newUnivers = await univers.ApplyTo(new Univers(), dbContext);
        dbContext.Univers.Add(newUnivers);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(UniversSubscriptions.UniversCreated), newUnivers, cancellationToken);
        return newUnivers;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Univers> UpdateUnivers(UniversUpdateInput univers, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Univers? oldUnivers = await dbContext.Univers.SingleOrDefaultAsync(p => p.Id == univers.Id, cancellationToken);
        if (oldUnivers is null)
            throw new NotFoundIdException(univers.Id);
        if (univers.Nom.HasValue && await dbContext.Univers.AnyAsync(g => g.Id != oldUnivers.Id && g.Nom == univers.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Univers name \"{univers.Nom.Value}\" is already used");

        await univers.ApplyTo(oldUnivers, dbContext);
        dbContext.Update(oldUnivers);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(UniversSubscriptions.UniversUpdated), oldUnivers, cancellationToken);
        return oldUnivers;
    }

    [Error<NotFoundIdException>]
    public static async Task<Univers> DeleteUnivers([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Univers? univers = await dbContext.Univers.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (univers is null)
            throw new NotFoundIdException(id);

        dbContext.Univers.Remove(univers);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(UniversSubscriptions.UniversDeleted), univers, cancellationToken);
        return univers;
    }
}
