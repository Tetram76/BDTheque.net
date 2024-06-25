namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static class PersonneSubscriptions
{
    [Subscribe(With = nameof(PersonneCreatedStream))]
    public static Personne PersonneCreated([EventMessage] Personne personne) => personne;

    [Subscribe(With = nameof(PersonneUpdatedStream))]
    public static Personne PersonneUpdated([EventMessage] Personne personne) => personne;

    [Subscribe(With = nameof(PersonneDeletedStream))]
    public static Personne PersonneDeleted([EventMessage] Personne personne) => personne;

    private static async IAsyncEnumerable<Personne> PersonneCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Personne> sourceStream = await eventReceiver.SubscribeAsync<Personne>(nameof(PersonneCreated), cancellationToken);
        await foreach (Personne personne in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return personne;
    }

    private static async IAsyncEnumerable<Personne> PersonneUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Personne> sourceStream = await eventReceiver.SubscribeAsync<Personne>(nameof(PersonneUpdated), cancellationToken);
        await foreach (Personne personne in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return personne;
    }

    private static async IAsyncEnumerable<Personne> PersonneDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Personne> sourceStream = await eventReceiver.SubscribeAsync<Personne>(nameof(PersonneDeleted), cancellationToken);
        await foreach (Personne personne in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return personne;
    }
}
