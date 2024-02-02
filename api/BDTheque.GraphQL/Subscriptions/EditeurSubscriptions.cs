namespace BDTheque.GraphQL.Subscriptions;

using System.Runtime.CompilerServices;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Types;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

[SubscriptionType]
public static partial class EditeurSubscriptions
{
    [Subscribe(With = nameof(EditeurCreatedStream))]
    [GraphQLType<EditeurType>]
    public static Editeur EditeurCreated([EventMessage] Editeur editeur) => editeur;

    [Subscribe(With = nameof(EditeurUpdatedStream))]
    [GraphQLType<EditeurType>]
    public static Editeur EditeurUpdated([EventMessage] Editeur editeur) => editeur;

    [Subscribe(With = nameof(EditeurDeletedStream))]
    [GraphQLType<EditeurType>]
    public static Editeur EditeurDeleted([EventMessage] Editeur editeur) => editeur;

    private static async IAsyncEnumerable<Editeur> EditeurCreatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Editeur> sourceStream = await eventReceiver.SubscribeAsync<Editeur>(nameof(EditeurCreated), cancellationToken);
        await foreach (Editeur editeur in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return editeur;
    }

    private static async IAsyncEnumerable<Editeur> EditeurUpdatedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Editeur> sourceStream = await eventReceiver.SubscribeAsync<Editeur>(nameof(EditeurUpdated), cancellationToken);
        await foreach (Editeur editeur in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return editeur;
    }

    private static async IAsyncEnumerable<Editeur> EditeurDeletedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ISourceStream<Editeur> sourceStream = await eventReceiver.SubscribeAsync<Editeur>(nameof(EditeurDeleted), cancellationToken);
        await foreach (Editeur editeur in sourceStream.ReadEventsAsync().WithCancellation(cancellationToken))
            yield return editeur;
    }
}
