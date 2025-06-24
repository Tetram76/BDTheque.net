namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class PersonneSubscriptions
{
    [Subscribe(With = nameof(PersonneCreatedStream))]
    public static async Task<Personne> PersonneCreated([EventMessage] Guid personneId, [Service] IPersonneByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(personneId, cancellationToken);

    [Subscribe(With = nameof(PersonneUpdatedStream))]
    public static async Task<Personne> PersonneUpdated([EventMessage] Guid personneId, [Service] IPersonneByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(personneId, cancellationToken);

    [Subscribe(With = nameof(PersonneDeletedStream))]
    public static async Task<Personne> PersonneDeleted([EventMessage] Guid personneId, [Service] IPersonneByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(personneId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> PersonneCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(PersonneCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> PersonneUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(PersonneUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> PersonneDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(PersonneDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
