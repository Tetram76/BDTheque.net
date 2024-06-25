namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;

using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class SerieMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Serie> CreateSerie(SerieCreateInput serie, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie newSerie = serie.BuildEntity<Serie>();
        dbContext.Series.Add(newSerie);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(SerieSubscriptions.SerieCreated), newSerie, cancellationToken);
        return newSerie;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Serie> UpdateSerie(SerieUpdateInput serie, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie? oldSerie = await dbContext.Series.Where(p => p.Id == serie.Id).SingleOrDefaultAsync(cancellationToken);
        if (oldSerie is null)
            throw new NotFoundIdException();

        serie.ApplyUpdate(oldSerie);
        dbContext.Update(oldSerie);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(SerieSubscriptions.SerieUpdated), oldSerie, cancellationToken);
        return oldSerie;
    }

    [Error<NotFoundIdException>]
    public static async Task<Serie> DeleteSerie([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie? serie = await dbContext.Series.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (serie is null)
            throw new NotFoundIdException();

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
