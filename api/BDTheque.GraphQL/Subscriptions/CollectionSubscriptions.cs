namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class CollectionSubscriptions
{
    [Subscribe(With = nameof(CollectionCreatedStream))]
    public static async Task<Collection> CollectionCreated([EventMessage] Guid collectionId, [Service] ICollectionByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(collectionId, cancellationToken);

    [Subscribe(With = nameof(CollectionUpdatedStream))]
    public static async Task<Collection> CollectionUpdated([EventMessage] Guid collectionId, [Service] ICollectionByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(collectionId, cancellationToken);

    [Subscribe(With = nameof(CollectionDeletedStream))]
    public static async Task<Collection> CollectionDeleted([EventMessage] Guid collectionId, [Service] ICollectionByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(collectionId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> CollectionCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(CollectionCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> CollectionUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(CollectionUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> CollectionDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(CollectionDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
