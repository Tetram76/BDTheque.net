namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Univers>]
public static partial class UniversMutations
{
    [Error<ValidationException>]
    public static async Task<Univers> CreateUnivers(UniversCreateInput univers, [Service] IUniversRepository universRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Univers newUnivers = await univers.ApplyTo(new Univers(), universRepository.DbContext);
        await universRepository.Add(newUnivers, cancellationToken);

        await sender.SendAsync(nameof(UniversSubscriptions.UniversCreated), newUnivers, cancellationToken);
        return newUnivers;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Univers> UpdateUnivers(UniversUpdateInput univers, [Service] IUniversRepository universRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Univers oldUnivers = await universRepository.GetById(univers.Id, cancellationToken);

        await univers.ApplyTo(oldUnivers, universRepository.DbContext);
        await universRepository.Update(oldUnivers, cancellationToken);

        await sender.SendAsync(nameof(UniversSubscriptions.UniversUpdated), oldUnivers, cancellationToken);
        return oldUnivers;
    }

    [Error<InvalidOperationException>]
    public static async Task<Univers> DeleteUnivers([ID] Guid id, [Service] IUniversRepository universRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Univers univers = await universRepository.GetById(id, cancellationToken);

        await universRepository.Remove(univers, cancellationToken);

        await sender.SendAsync(nameof(UniversSubscriptions.UniversDeleted), univers, cancellationToken);
        return univers;
    }
}
