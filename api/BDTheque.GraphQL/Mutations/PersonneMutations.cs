namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.GraphQL.Types;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[MutationType]
public static class PersonneMutations
{
    // createPersonne(data: PersonneCreateInput!): Personne!
    // updatePersonne(data: PersonneUpdateInput!): Personne!

    [Error<NotFoundIdException>]
    [GraphQLType<PersonneType>]
    public static async Task<Personne> DeletePersonne([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Personne? personne = await dbContext.Personnes.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (personne is null)
            throw new NotFoundIdException();
        dbContext.Personnes.Remove(personne);
        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneDeleted), personne, cancellationToken);
        return personne;
    }
}
