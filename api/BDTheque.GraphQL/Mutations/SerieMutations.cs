namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [MutationType]
public static class SerieMutations
{
    // createSerie(data: SerieCreateInput!): Serie!
    // updateSerie(data: SerieUpdateInput!): Serie!

    [Error<NotFoundIdException>]
    [GraphQLType<SerieType>]
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
