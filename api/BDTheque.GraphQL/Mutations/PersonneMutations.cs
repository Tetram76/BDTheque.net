namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class PersonneMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Personne> CreatePersonne(PersonneCreateInput personne, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Personnes.AnyAsync(g => g.Nom == personne.Nom, cancellationToken))
            throw new AlreadyExistsException();

        Personne newPersonne = personne.BuildEntity<Personne>();
        dbContext.Personnes.Add(newPersonne);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneCreated), newPersonne, cancellationToken);
        return newPersonne;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Personne> UpdatePersonne(PersonneUpdateInput personne, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Personne? oldPersonne = await dbContext.Personnes.Where(p => p.Id == personne.Id).SingleOrDefaultAsync(cancellationToken);
        if (oldPersonne is null)
            throw new NotFoundIdException();
        if (personne.Nom.HasValue && await dbContext.Personnes.AnyAsync(g => g.Id != oldPersonne.Id && g.Nom == personne.Nom, cancellationToken))
            throw new AlreadyExistsException();

        personne.ApplyUpdate(oldPersonne);
        dbContext.Update(oldPersonne);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneUpdated), oldPersonne, cancellationToken);
        return oldPersonne;
    }

    [Error<NotFoundIdException>]
    public static async Task<Personne> DeletePersonne([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Personne? Personne = await dbContext.Personnes.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (Personne is null)
            throw new NotFoundIdException();

        dbContext.Personnes.Remove(Personne);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneDeleted), Personne, cancellationToken);
        return Personne;
    }
}
