namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Types;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static class EditionSubscriptions
{
    [Subscribe(With = nameof(EditionCreatedStream))]
    [GraphQLType<EditionType>]
    public static EditionAlbum EditionCreated([EventMessage] EditionAlbum edition) => edition;

    [Subscribe(With = nameof(EditionUpdatedStream))]
    [GraphQLType<EditionType>]
    public static EditionAlbum EditionUpdated([EventMessage] EditionAlbum edition) => edition;

    [Subscribe(With = nameof(EditionDeletedStream))]
    [GraphQLType<EditionType>]
    public static EditionAlbum EditionDeleted([EventMessage] EditionAlbum edition) => edition;

    private static async IAsyncEnumerable<EditionAlbum> EditionCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<EditionAlbum> sourceStream = await eventReceiver.SubscribeAsync<EditionAlbum>(nameof(EditionCreated), cancellationToken);
        await foreach (EditionAlbum edition in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return edition;
    }

    private static async IAsyncEnumerable<EditionAlbum> EditionUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<EditionAlbum> sourceStream = await eventReceiver.SubscribeAsync<EditionAlbum>(nameof(EditionUpdated), cancellationToken);
        await foreach (EditionAlbum edition in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return edition;
    }

    private static async IAsyncEnumerable<EditionAlbum> EditionDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<EditionAlbum> sourceStream = await eventReceiver.SubscribeAsync<EditionAlbum>(nameof(EditionDeleted), cancellationToken);
        await foreach (EditionAlbum edition in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return edition;
    }
}
