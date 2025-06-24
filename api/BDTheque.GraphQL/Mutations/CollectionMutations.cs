namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;

using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Collection>]
public static partial class CollectionMutations
{
    [Error<ValidationException>]
    public static async Task<Collection> CreateCollection(CollectionCreateInput collection, [Service] ICollectionRepository collectionRepository, [Service] IServiceProvider serviceProvider, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection newCollection = await collection.ApplyTo(new Collection(), serviceProvider, cancellationToken);
        await collectionRepository.Add(newCollection, cancellationToken);

        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionCreatedStream), newCollection.Id, cancellationToken);
        return newCollection;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Collection> UpdateCollection(CollectionUpdateInput collection, [Service] ICollectionRepository collectionRepository, [Service] IServiceProvider serviceProvider, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection oldCollection = await collectionRepository.GetById(collection.Id, cancellationToken);

        await collection.ApplyTo(oldCollection, serviceProvider, cancellationToken);
        await collectionRepository.Update(oldCollection, cancellationToken);

        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionUpdatedStream), oldCollection.Id, cancellationToken);
        return oldCollection;
    }

    [Error<InvalidOperationException>]
    public static async Task<Collection> DeleteCollection([ID] Guid id, [Service] ICollectionRepository collectionRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Collection collection = await collectionRepository.GetById(id, cancellationToken);

        await collectionRepository.Remove(collection, cancellationToken);

        await sender.SendAsync(nameof(CollectionSubscriptions.CollectionDeletedStream), collection.Id, cancellationToken);
        return collection;
    }
}
