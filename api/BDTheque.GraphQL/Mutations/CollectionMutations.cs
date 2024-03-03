namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class CollectionMutations
{
    private static Task<Collection> ApplyTo(this ICollectionInputType input, Collection collection, BDThequeContext dbContext, CancellationToken cancellationToken) =>
        input.ApplyTo(
            collection,
            async editeur => await dbContext.Editeurs.SingleAsync(e => e.Id == editeur.Id, cancellationToken)
        );

    [Error<AlreadyExistsException>]
    [Error<InvalidOperationException>]
    public static async Task<Collection> CreateCollection(CollectionCreateInput collection, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Collections.AnyAsync(g => g.Nom == collection.Nom.Value && g.EditeurId == collection.Editeur.Value!.Id, cancellationToken))
            throw new AlreadyExistsException($"Collection name \"{collection.Nom.Value}\" is already used");

        Collection newCollection = await collection.ApplyTo(new Collection(), dbContext, cancellationToken);
        dbContext.Collections.Add(newCollection);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionCreated), newCollection, cancellationToken);
        return newCollection;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    [Error<InvalidOperationException>]
    public static async Task<Collection> UpdateCollection(CollectionUpdateInput collection, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection? oldCollection = await dbContext.Collections.Where(p => p.Id == collection.Id).SingleOrDefaultAsync(cancellationToken);
        if (oldCollection is null)
            throw new NotFoundIdException(collection.Id);
        Guid newEditeurId = collection.Editeur.HasValue ? collection.Editeur.Value.Id : oldCollection.EditeurId;
        if (collection.Nom.HasValue && await dbContext.Collections.AnyAsync(g => g.Id != oldCollection.Id && g.EditeurId == newEditeurId && g.Nom == collection.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Collection name \"{collection.Nom.Value}\" is already used");

        await collection.ApplyTo(oldCollection, dbContext, cancellationToken);
        dbContext.Update(oldCollection);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionUpdated), oldCollection, cancellationToken);
        return oldCollection;
    }

    [Error<NotFoundIdException>]
    public static async Task<Collection> DeleteCollection([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection? collection = await dbContext.Collections.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (collection is null)
            throw new NotFoundIdException(id);

        dbContext.Collections.Remove(collection);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionDeleted), collection, cancellationToken);
        return collection;
    }
}
