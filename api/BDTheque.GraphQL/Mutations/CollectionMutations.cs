namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [MutationType]
public static class CollectionMutations
{
    // createCollection(data: CollectionCreateInput!): Collection!
    // updateCollection(data: CollectionUpdateInput!): Collection!

    [Error<NotFoundIdException>]
    [GraphQLType<CollectionType>]
    public static async Task<Collection> DeleteCollection([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection? collection = await dbContext.Collections.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (collection is null)
            throw new NotFoundIdException();
        dbContext.Collections.Remove(collection);
        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionDeleted), collection, cancellationToken);
        return collection;
    }
}
