namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class AlbumSubscriptions
{
    [Subscribe(With = nameof(AlbumCreatedStream))]
    public static async Task<Album> AlbumCreated([EventMessage] Guid albumId, [Service] IAlbumByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(albumId, cancellationToken);

    [Subscribe(With = nameof(AlbumUpdatedStream))]
    public static async Task<Album> AlbumUpdated([EventMessage] Guid albumId, [Service] IAlbumByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(albumId, cancellationToken);

    [Subscribe(With = nameof(AlbumDeletedStream))]
    public static async Task<Album> AlbumDeleted([EventMessage] Guid albumId, [Service] IAlbumByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(albumId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> AlbumCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(AlbumCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> AlbumUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(AlbumUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> AlbumDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(AlbumDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
