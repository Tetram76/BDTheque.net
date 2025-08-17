namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class UniversSubscriptions
{
    [Subscribe(With = nameof(UniversCreatedStream))]
    public static async Task<Univers> UniversCreated([EventMessage] Guid universId, [Service] IUniversByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(universId, cancellationToken);

    [Subscribe(With = nameof(UniversUpdatedStream))]
    public static async Task<Univers> UniversUpdated([EventMessage] Guid universId, [Service] IUniversByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(universId, cancellationToken);

    [Subscribe(With = nameof(UniversDeletedStream))]
    public static async Task<Univers> UniversDeleted([EventMessage] Guid universId, [Service] IUniversByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(universId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> UniversCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(UniversCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> UniversUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(UniversUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> UniversDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(UniversDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
