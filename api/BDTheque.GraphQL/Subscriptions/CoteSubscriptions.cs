namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class CoteSubscriptions
{
    [Subscribe(With = nameof(CoteCreatedStream))]
    public static async Task<Cote> CoteCreated([EventMessage] Guid coteId, [Service] ICoteByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(coteId, cancellationToken);

    [Subscribe(With = nameof(CoteUpdatedStream))]
    public static async Task<Cote> CoteUpdated([EventMessage] Guid coteId, [Service] ICoteByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(coteId, cancellationToken);

    [Subscribe(With = nameof(CoteDeletedStream))]
    public static async Task<Cote> CoteDeleted([EventMessage] Guid coteId, [Service] ICoteByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(coteId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> CoteCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(CoteCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> CoteUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(CoteUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> CoteDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(CoteDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
