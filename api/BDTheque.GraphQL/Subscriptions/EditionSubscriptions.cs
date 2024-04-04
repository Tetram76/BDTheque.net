namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class EditionSubscriptions
{
    [Subscribe(With = nameof(EditionCreatedStream))]
    public static async Task<Edition> EditionCreated([EventMessage] Guid editionId, [Service] IEditionByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editionId, cancellationToken);

    [Subscribe(With = nameof(EditionUpdatedStream))]
    public static async Task<Edition> EditionUpdated([EventMessage] Guid editionId, [Service] IEditionByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editionId, cancellationToken);

    [Subscribe(With = nameof(EditionDeletedStream))]
    public static async Task<Edition> EditionDeleted([EventMessage] Guid editionId, [Service] IEditionByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editionId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> EditionCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(EditionCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> EditionUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(EditionUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> EditionDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(EditionDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
