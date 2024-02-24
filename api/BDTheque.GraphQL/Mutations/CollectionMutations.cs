namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class CollectionMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Collection> CreateCollection(CollectionCreateInput collection, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Collections.AnyAsync(g => g.Nom == collection.Nom, cancellationToken))
            throw new AlreadyExistsException();

        Collection newCollection = collection.BuildEntity<Collection>();
        dbContext.Collections.Add(newCollection);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionCreated), newCollection, cancellationToken);
        return newCollection;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Collection> UpdateCollection(CollectionUpdateInput collection, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection? oldCollection = await dbContext.Collections.Where(p => p.Id == collection.Id).SingleOrDefaultAsync(cancellationToken);
        if (oldCollection is null)
            throw new NotFoundIdException();
        if (collection.Nom.HasValue && await dbContext.Collections.AnyAsync(g => g.Id != oldCollection.Id && g.Nom == collection.Nom, cancellationToken))
            throw new AlreadyExistsException();

        collection.ApplyUpdate(oldCollection);
        dbContext.Update(oldCollection);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionUpdated), oldCollection, cancellationToken);
        return oldCollection;
    }

    [Error<NotFoundIdException>]
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
