namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Extensions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Editeur>]
public static partial class EditeurMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Editeur> CreateEditeur(EditeurCreateInput editeur, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Editeurs.AnyAsync(g => g.Nom == editeur.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Editeur name \"{editeur.Nom.Value}\" is already used");

        Editeur newEditeur = (editeur as IEditeurInputType).ApplyTo(new Editeur());
        newEditeur.Validate();
        dbContext.Editeurs.Add(newEditeur);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurCreated), newEditeur, cancellationToken);
        return newEditeur;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Editeur> UpdateEditeur(EditeurUpdateInput editeur, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Editeur? oldEditeur = await dbContext.Editeurs.SingleOrDefaultAsync(p => p.Id == editeur.Id, cancellationToken);
        if (oldEditeur is null)
            throw new NotFoundIdException(editeur.Id);
        if (editeur.Nom.HasValue && await dbContext.Editeurs.AnyAsync(g => g.Id != oldEditeur.Id && g.Nom == editeur.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Editeur name \"{editeur.Nom.Value}\" is already used");

        (editeur as IEditeurInputType).ApplyTo(oldEditeur);
        dbContext.Update(oldEditeur);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurUpdated), oldEditeur, cancellationToken);
        return oldEditeur;
    }

    [Error<NotFoundIdException>]
    public static async Task<Editeur> DeleteEditeur([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Editeur? editeur = await dbContext.Editeurs.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (editeur is null)
            throw new NotFoundIdException(id);

        dbContext.Editeurs.Remove(editeur);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurDeleted), editeur, cancellationToken);
        return editeur;
    }
}
