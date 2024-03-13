namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Serie>]
public static partial class SerieMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Serie> CreateSerie(SerieCreateInput serie, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie newSerie = await serie.ApplyTo(new Serie(), dbContext);
        dbContext.Series.Add(newSerie);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(SerieSubscriptions.SerieCreated), newSerie, cancellationToken);
        return newSerie;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Serie> UpdateSerie(SerieUpdateInput serie, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie? oldSerie = await dbContext.Series.SingleOrDefaultAsync(p => p.Id == serie.Id, cancellationToken);
        if (oldSerie is null)
            throw new NotFoundIdException(serie.Id);

        await serie.ApplyTo(oldSerie, dbContext);
        dbContext.Update(oldSerie);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(SerieSubscriptions.SerieUpdated), oldSerie, cancellationToken);
        return oldSerie;
    }

    [Error<NotFoundIdException>]
    public static async Task<Serie> DeleteSerie([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie? serie = await dbContext.Series.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (serie is null)
            throw new NotFoundIdException(id);

        dbContext.Series.Remove(serie);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(SerieSubscriptions.SerieDeleted), serie, cancellationToken);
        return serie;
    }

    // addSerieAuteur(metier: Metier!, personneId: UUID!, serieId: UUID!): Serie!
    // removeSerieAuteur(metier: Metier!, personneId: UUID!, serieId: UUID!): Serie!
    // addSerieGenre(genre: String!, serieId: UUID!): Serie!
    // removeSerieGenre(genre: String!, serieId: UUID!): Serie!
}
