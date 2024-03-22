namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Collection>]
public static partial class CollectionMutations
{
    [Error<ValidationException>]
    public static async Task<Collection> CreateCollection(CollectionCreateInput collection, [Service] ICollectionRepository collectionRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection newCollection = await collection.ApplyTo(new Collection(), collectionRepository.DbContext, cancellationToken);
        await collectionRepository.Add(newCollection, cancellationToken);

        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionCreated), newCollection, cancellationToken);
        return newCollection;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Collection> UpdateCollection(CollectionUpdateInput collection, [Service] ICollectionRepository collectionRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection oldCollection = await collectionRepository.GetById(collection.Id, cancellationToken);

        await collection.ApplyTo(oldCollection, collectionRepository.DbContext, cancellationToken);
        await collectionRepository.Update(oldCollection, cancellationToken);

        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionUpdated), oldCollection, cancellationToken);
        return oldCollection;
    }

    [Error<InvalidOperationException>]
    public static async Task<Collection> DeleteCollection([ID] Guid id, [Service] ICollectionRepository collectionRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection collection = await collectionRepository.GetById(id, cancellationToken);

        await collectionRepository.Remove(collection, cancellationToken);

        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionDeleted), collection, cancellationToken);
        return collection;
    }
}
