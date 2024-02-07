namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static class SerieSubscriptions
{
    [Subscribe(With = nameof(SerieCreatedStream))]
    public static Serie SerieCreated([EventMessage] Serie serie) => serie;

    [Subscribe(With = nameof(SerieUpdatedStream))]
    public static Serie SerieUpdated([EventMessage] Serie serie) => serie;

    [Subscribe(With = nameof(SerieDeletedStream))]
    public static Serie SerieDeleted([EventMessage] Serie serie) => serie;

    private static async IAsyncEnumerable<Serie> SerieCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Serie> sourceStream = await eventReceiver.SubscribeAsync<Serie>(nameof(SerieCreated), cancellationToken);
        await foreach (Serie serie in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return serie;
    }

    private static async IAsyncEnumerable<Serie> SerieUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Serie> sourceStream = await eventReceiver.SubscribeAsync<Serie>(nameof(SerieUpdated), cancellationToken);
        await foreach (Serie serie in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return serie;
    }

    private static async IAsyncEnumerable<Serie> SerieDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Serie> sourceStream = await eventReceiver.SubscribeAsync<Serie>(nameof(SerieDeleted), cancellationToken);
        await foreach (Serie serie in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return serie;
    }
}
