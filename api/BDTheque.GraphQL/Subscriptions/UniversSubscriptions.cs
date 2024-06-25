namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static class UniversSubscriptions
{
    [Subscribe(With = nameof(UniversCreatedStream))]
    public static Univers UniversCreated([EventMessage] Univers univers) => univers;

    [Subscribe(With = nameof(UniversUpdatedStream))]
    public static Univers UniversUpdated([EventMessage] Univers univers) => univers;

    [Subscribe(With = nameof(UniversDeletedStream))]
    public static Univers UniversDeleted([EventMessage] Univers univers) => univers;

    private static async IAsyncEnumerable<Univers> UniversCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Univers> sourceStream = await eventReceiver.SubscribeAsync<Univers>(nameof(UniversCreated), cancellationToken);
        await foreach (Univers univers in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return univers;
    }

    private static async IAsyncEnumerable<Univers> UniversUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Univers> sourceStream = await eventReceiver.SubscribeAsync<Univers>(nameof(UniversUpdated), cancellationToken);
        await foreach (Univers univers in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return univers;
    }

    private static async IAsyncEnumerable<Univers> UniversDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Univers> sourceStream = await eventReceiver.SubscribeAsync<Univers>(nameof(UniversDeleted), cancellationToken);
        await foreach (Univers univers in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return univers;
    }
}
