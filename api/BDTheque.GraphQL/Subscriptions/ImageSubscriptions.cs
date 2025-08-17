namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class ImageSubscriptions
{
    [Subscribe(With = nameof(ImageCreatedStream))]
    public static async Task<Image> ImageCreated([EventMessage] Guid imageId, [Service] IImageByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(imageId, cancellationToken);

    [Subscribe(With = nameof(ImageUpdatedStream))]
    public static async Task<Image> ImageUpdated([EventMessage] Guid imageId, [Service] IImageByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(imageId, cancellationToken);

    [Subscribe(With = nameof(ImageDeletedStream))]
    public static async Task<Image> ImageDeleted([EventMessage] Guid imageId, [Service] IImageByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(imageId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> ImageCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(ImageCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> ImageUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(ImageUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> ImageDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(ImageDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
