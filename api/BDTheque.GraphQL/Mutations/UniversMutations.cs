namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class UniversMutations
{
    // createUnivers(data: UniversCreateInput!): Univers!
    // updateUnivers(data: UniversUpdateInput!): Univers!

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
