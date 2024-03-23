namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Personne>]
public static partial class PersonneMutations
{
    [Error<ValidationException>]
    public static async Task<Personne> CreatePersonne(PersonneCreateInput personne, [Service] IPersonneRepository personneRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Personne newPersonne = (personne as IPersonneInputType).ApplyTo(new Personne());
        await personneRepository.Add(newPersonne, cancellationToken);

        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneCreated), newPersonne, cancellationToken);
        return newPersonne;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Personne> UpdatePersonne(PersonneUpdateInput personne, [Service] IPersonneRepository personneRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Personne oldPersonne = await personneRepository.GetById(personne.Id, cancellationToken);

        (personne as IPersonneInputType).ApplyTo(oldPersonne);
        await personneRepository.Update(oldPersonne, cancellationToken);

        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneUpdated), oldPersonne, cancellationToken);
        return oldPersonne;
    }

    [Error<InvalidOperationException>]
    public static async Task<Personne> DeletePersonne([ID] Guid id, [Service] IPersonneRepository personneRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Personne personne = await personneRepository.GetById(id, cancellationToken);

        await personneRepository.Remove(personne, cancellationToken);

        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneDeleted), personne, cancellationToken);
        return personne;
    }
}
