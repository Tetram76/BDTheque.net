namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.GraphQL.Types;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[MutationType]
public static class EditeurMutations
{
    // createEditeur(data: EditeurCreateInput!): Editeur!
    // updateEditeur(data: EditeurUpdateInput!): Editeur!

    [Error<NotFoundIdException>]
    [GraphQLType<EditeurType>]
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
