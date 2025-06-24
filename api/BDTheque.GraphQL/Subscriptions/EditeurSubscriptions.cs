namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class EditeurSubscriptions
{
    [Subscribe(With = nameof(EditeurCreatedStream))]
    public static async Task<Editeur> EditeurCreated([EventMessage] Guid editeurId, [Service] IEditeurByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editeurId, cancellationToken);

    [Subscribe(With = nameof(EditeurUpdatedStream))]
    public static async Task<Editeur> EditeurUpdated([EventMessage] Guid editeurId, [Service] IEditeurByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editeurId, cancellationToken);

    [Subscribe(With = nameof(EditeurDeletedStream))]
    public static async Task<Editeur> EditeurDeleted([EventMessage] Guid editeurId, [Service] IEditeurByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(editeurId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> EditeurCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(EditeurCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> EditeurUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(EditeurUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> EditeurDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(EditeurDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
