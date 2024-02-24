namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class EditeurMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Editeur> CreateEditeur(EditeurCreateInput editeur, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Editeurs.AnyAsync(g => g.Nom == editeur.Nom, cancellationToken))
            throw new AlreadyExistsException();

        Editeur newEditeur = editeur.BuildEntity<Editeur>();
        dbContext.Editeurs.Add(newEditeur);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurCreated), newEditeur, cancellationToken);
        return newEditeur;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Editeur> UpdateEditeur(EditeurUpdateInput editeur, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Editeur? oldEditeur = await dbContext.Editeurs.Where(p => p.Id == editeur.Id).SingleOrDefaultAsync(cancellationToken);
        if (oldEditeur is null)
            throw new NotFoundIdException();
        if (editeur.Nom.HasValue && await dbContext.Editeurs.AnyAsync(g => g.Id != oldEditeur.Id && g.Nom == editeur.Nom, cancellationToken))
            throw new AlreadyExistsException();

        editeur.ApplyUpdate(oldEditeur);
        dbContext.Update(oldEditeur);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurUpdated), oldEditeur, cancellationToken);
        return oldEditeur;
    }

    [Error<NotFoundIdException>]
    public static async Task<Editeur> DeleteEditeur([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Editeur? editeur = await dbContext.Editeurs.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (editeur is null)
            throw new NotFoundIdException();

        dbContext.Editeurs.Remove(editeur);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(EditeurSubscriptions.EditeurDeleted), editeur, cancellationToken);
        return editeur;
    }
}
