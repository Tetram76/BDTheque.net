namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class SerieSubscriptions
{
    [Subscribe(With = nameof(SerieCreatedStream))]
    public static async Task<Serie> SerieCreated([EventMessage] Guid serieId, [Service] ISerieByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(serieId, cancellationToken);

    [Subscribe(With = nameof(SerieUpdatedStream))]
    public static async Task<Serie> SerieUpdated([EventMessage] Guid serieId, [Service] ISerieByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(serieId, cancellationToken);

    [Subscribe(With = nameof(SerieDeletedStream))]
    public static async Task<Serie> SerieDeleted([EventMessage] Guid serieId, [Service] ISerieByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(serieId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> SerieCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(SerieCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> SerieUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(SerieUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> SerieDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(SerieDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
