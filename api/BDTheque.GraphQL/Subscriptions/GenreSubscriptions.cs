namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;

using BDTheque.GraphQL.DataLoaders;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SubscriptionType]
public static class GenreSubscriptions
{
    [Subscribe(With = nameof(GenreCreatedStream))]
    public static async Task<Genre> GenreCreated([EventMessage] Guid genreId, [Service] IGenreByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(genreId, cancellationToken);

    [Subscribe(With = nameof(GenreUpdatedStream))]
    public static async Task<Genre> GenreUpdated([EventMessage] Guid genreId, [Service] IGenreByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(genreId, cancellationToken);

    [Subscribe(With = nameof(GenreDeletedStream))]
    public static async Task<Genre> GenreDeleted([EventMessage] Guid genreId, [Service] IGenreByIdDataLoader loader, CancellationToken cancellationToken) =>
        await loader.LoadAsync(genreId, cancellationToken);

    internal static async IAsyncEnumerable<Guid> GenreCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(GenreCreatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> GenreUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(GenreUpdatedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }

    internal static async IAsyncEnumerable<Guid> GenreDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Guid> sourceStream = await eventReceiver.SubscribeAsync<Guid>(nameof(GenreDeletedStream), cancellationToken);
        await foreach (Guid id in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return id;
    }
}
