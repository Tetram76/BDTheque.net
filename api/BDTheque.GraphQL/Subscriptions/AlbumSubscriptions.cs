namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static class AlbumSubscriptions
{
    [Subscribe(With = nameof(AlbumCreatedStream))]
    public static Album AlbumCreated([EventMessage] Album album) => album;

    [Subscribe(With = nameof(AlbumUpdatedStream))]
    public static Album AlbumUpdated([EventMessage] Album album) => album;

    [Subscribe(With = nameof(AlbumDeletedStream))]
    public static Album AlbumDeleted([EventMessage] Album album) => album;

    [Subscribe(With = nameof(CoteAlbumCreatedStream))]
    public static CoteEdition CoteAlbumCreated([EventMessage] CoteEdition coteAlbum) => coteAlbum;

    [Subscribe(With = nameof(CoteAlbumUpdatedStream))]
    public static CoteEdition CoteAlbumUpdated([EventMessage] CoteEdition coteAlbum) => coteAlbum;

    [Subscribe(With = nameof(CoteAlbumDeletedStream))]
    public static CoteEdition CoteAlbumDeleted([EventMessage] CoteEdition coteAlbum) => coteAlbum;

    private static async IAsyncEnumerable<Album> AlbumCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Album> sourceStream = await eventReceiver.SubscribeAsync<Album>(nameof(AlbumCreated), cancellationToken);
        await foreach (Album album in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return album;
    }

    private static async IAsyncEnumerable<Album> AlbumUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Album> sourceStream = await eventReceiver.SubscribeAsync<Album>(nameof(AlbumUpdated), cancellationToken);
        await foreach (Album album in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return album;
    }

    private static async IAsyncEnumerable<Album> AlbumDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Album> sourceStream = await eventReceiver.SubscribeAsync<Album>(nameof(AlbumDeleted), cancellationToken);
        await foreach (Album album in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return album;
    }

    private static async IAsyncEnumerable<CoteEdition> CoteAlbumCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<CoteEdition> sourceStream = await eventReceiver.SubscribeAsync<CoteEdition>(nameof(CoteAlbumCreated), cancellationToken);
        await foreach (CoteEdition coteAlbum in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return coteAlbum;
    }

    private static async IAsyncEnumerable<CoteEdition> CoteAlbumUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<CoteEdition> sourceStream = await eventReceiver.SubscribeAsync<CoteEdition>(nameof(CoteAlbumUpdated), cancellationToken);
        await foreach (CoteEdition coteAlbum in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return coteAlbum;
    }

    private static async IAsyncEnumerable<CoteEdition> CoteAlbumDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<CoteEdition> sourceStream = await eventReceiver.SubscribeAsync<CoteEdition>(nameof(CoteAlbumDeleted), cancellationToken);
        await foreach (CoteEdition coteAlbum in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return coteAlbum;
    }
}
