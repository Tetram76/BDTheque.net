namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;

using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Editeur>]
public static partial class EditeurMutations
{
    [Error<ValidationException>]
    public static async Task<Editeur> CreateEditeur(EditeurCreateInput editeur, [Service] IEditeurRepository editeurRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Editeur newEditeur = (editeur as IEditeurInputType).ApplyTo(new Editeur());
        await editeurRepository.Add(newEditeur, cancellationToken);

        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurCreatedStream), newEditeur.Id, cancellationToken);
        return newEditeur;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Editeur> UpdateEditeur(EditeurUpdateInput editeur, [Service] IEditeurRepository editeurRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Editeur oldEditeur = await editeurRepository.GetById(editeur.Id, cancellationToken);

        (editeur as IEditeurInputType).ApplyTo(oldEditeur);
        await editeurRepository.Update(oldEditeur, cancellationToken);

        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurUpdatedStream), oldEditeur.Id, cancellationToken);
        return oldEditeur;
    }

    [Error<InvalidOperationException>]
    public static async Task<Editeur> DeleteEditeur([ID] Guid id, [Service] IEditeurRepository editeurRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Editeur editeur = await editeurRepository.GetById(id, cancellationToken);

        await editeurRepository.Remove(editeur, cancellationToken);

        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurDeletedStream), editeur.Id, cancellationToken);
        return editeur;
    }
}
