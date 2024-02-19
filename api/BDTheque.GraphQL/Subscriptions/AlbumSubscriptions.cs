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
    public static Cote CoteAlbumCreated([EventMessage] Cote coteAlbum) => coteAlbum;

    [Subscribe(With = nameof(CoteAlbumUpdatedStream))]
    public static Cote CoteAlbumUpdated([EventMessage] Cote coteAlbum) => coteAlbum;

    [Subscribe(With = nameof(CoteAlbumDeletedStream))]
    public static Cote CoteAlbumDeleted([EventMessage] Cote coteAlbum) => coteAlbum;

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

    private static async IAsyncEnumerable<Cote> CoteAlbumCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Cote> sourceStream = await eventReceiver.SubscribeAsync<Cote>(nameof(CoteAlbumCreated), cancellationToken);
        await foreach (Cote coteAlbum in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return coteAlbum;
    }

    private static async IAsyncEnumerable<Cote> CoteAlbumUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Cote> sourceStream = await eventReceiver.SubscribeAsync<Cote>(nameof(CoteAlbumUpdated), cancellationToken);
        await foreach (Cote coteAlbum in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return coteAlbum;
    }

    private static async IAsyncEnumerable<Cote> CoteAlbumDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Cote> sourceStream = await eventReceiver.SubscribeAsync<Cote>(nameof(CoteAlbumDeleted), cancellationToken);
        await foreach (Cote coteAlbum in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return coteAlbum;
    }
}
