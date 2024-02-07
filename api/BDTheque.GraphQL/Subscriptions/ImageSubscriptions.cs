namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static class ImageSubscriptions
{
    [Subscribe(With = nameof(ImageCreatedStream))]
    public static Image ImageCreated([EventMessage] Image image) => image;

    [Subscribe(With = nameof(ImageUpdatedStream))]
    public static Image ImageUpdated([EventMessage] Image image) => image;

    [Subscribe(With = nameof(ImageDeletedStream))]
    public static Image ImageDeleted([EventMessage] Image image) => image;

    private static async IAsyncEnumerable<Image> ImageCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Image> sourceStream = await eventReceiver.SubscribeAsync<Image>(nameof(ImageCreated), cancellationToken);
        await foreach (Image image in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return image;
    }

    private static async IAsyncEnumerable<Image> ImageUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Image> sourceStream = await eventReceiver.SubscribeAsync<Image>(nameof(ImageUpdated), cancellationToken);
        await foreach (Image image in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return image;
    }

    private static async IAsyncEnumerable<Image> ImageDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Image> sourceStream = await eventReceiver.SubscribeAsync<Image>(nameof(ImageDeleted), cancellationToken);
        await foreach (Image image in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return image;
    }
}
