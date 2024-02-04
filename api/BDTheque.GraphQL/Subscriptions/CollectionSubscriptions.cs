namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static class CollectionSubscriptions
{
    [Subscribe(With = nameof(CollectionCreatedStream))]
    [GraphQLType<CollectionType>]
    public static Collection CollectionCreated([EventMessage] Collection collection) => collection;

    [Subscribe(With = nameof(CollectionUpdatedStream))]
    [GraphQLType<CollectionType>]
    public static Collection CollectionUpdated([EventMessage] Collection collection) => collection;

    [Subscribe(With = nameof(CollectionDeletedStream))]
    [GraphQLType<CollectionType>]
    public static Collection CollectionDeleted([EventMessage] Collection collection) => collection;

    private static async IAsyncEnumerable<Collection> CollectionCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Collection> sourceStream = await eventReceiver.SubscribeAsync<Collection>(nameof(CollectionCreated), cancellationToken);
        await foreach (Collection collection in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return collection;
    }

    private static async IAsyncEnumerable<Collection> CollectionUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Collection> sourceStream = await eventReceiver.SubscribeAsync<Collection>(nameof(CollectionUpdated), cancellationToken);
        await foreach (Collection collection in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return collection;
    }

    private static async IAsyncEnumerable<Collection> CollectionDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Collection> sourceStream = await eventReceiver.SubscribeAsync<Collection>(nameof(CollectionDeleted), cancellationToken);
        await foreach (Collection collection in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return collection;
    }
}
