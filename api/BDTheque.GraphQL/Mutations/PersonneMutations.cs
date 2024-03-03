namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class PersonneMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Personne> CreatePersonne(PersonneCreateInput personne, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Personnes.AnyAsync(g => g.Nom == personne.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Personne name \"{personne.Nom.Value}\" is already used");

        Personne newPersonne = (personne as IPersonneInputType).ApplyTo(new Personne());
        dbContext.Personnes.Add(newPersonne);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneCreated), newPersonne, cancellationToken);
        return newPersonne;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Personne> UpdatePersonne(PersonneUpdateInput personne, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Personne? oldPersonne = await dbContext.Personnes.SingleOrDefaultAsync(p => p.Id == personne.Id, cancellationToken);
        if (oldPersonne is null)
            throw new NotFoundIdException(personne.Id);
        if (personne.Nom.HasValue && await dbContext.Personnes.AnyAsync(g => g.Id != oldPersonne.Id && g.Nom == personne.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Personne name \"{personne.Nom.Value}\" is already used");

        (personne as IPersonneInputType).ApplyTo(oldPersonne);
        dbContext.Update(oldPersonne);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneUpdated), oldPersonne, cancellationToken);
        return oldPersonne;
    }

    [Error<NotFoundIdException>]
    public static async Task<Personne> DeletePersonne([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Personne? personne = await dbContext.Personnes.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (personne is null)
            throw new NotFoundIdException(id);

        dbContext.Personnes.Remove(personne);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(PersonneSubscriptions.PersonneDeleted), personne, cancellationToken);
        return personne;
    }
}
