namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static class GenreSubscriptions
{
    [Subscribe(With = nameof(GenreCreatedStream))]
    public static Genre GenreCreated([EventMessage] Genre genre) => genre;

    [Subscribe(With = nameof(GenreUpdatedStream))]
    public static Genre GenreUpdated([EventMessage] Genre genre) => genre;

    [Subscribe(With = nameof(GenreDeletedStream))]
    public static Genre GenreDeleted([EventMessage] Genre genre) => genre;

    private static async IAsyncEnumerable<Genre> GenreCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Genre> sourceStream = await eventReceiver.SubscribeAsync<Genre>(nameof(GenreCreated), cancellationToken);
        await foreach (Genre genre in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return genre;
    }

    private static async IAsyncEnumerable<Genre> GenreUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Genre> sourceStream = await eventReceiver.SubscribeAsync<Genre>(nameof(GenreUpdated), cancellationToken);
        await foreach (Genre genre in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return genre;
    }

    private static async IAsyncEnumerable<Genre> GenreDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Genre> sourceStream = await eventReceiver.SubscribeAsync<Genre>(nameof(GenreDeleted), cancellationToken);
        await foreach (Genre genre in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return genre;
    }
}
