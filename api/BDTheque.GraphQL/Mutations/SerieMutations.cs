namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Serie>]
public static partial class SerieMutations
{
    [Error<ValidationException>]
    public static async Task<Serie> CreateSerie(SerieCreateInput serie, [Service] ISerieRepository serieRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie newSerie = await serie.ApplyTo(new Serie(), serieRepository.DbContext);
        await serieRepository.Add(newSerie, cancellationToken);

        await sender.SendAsync(nameof(SerieSubscriptions.SerieCreated), newSerie, cancellationToken);
        return newSerie;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Serie> UpdateSerie(SerieUpdateInput serie, [Service] ISerieRepository serieRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie oldSerie = await serieRepository.GetById(serie.Id, cancellationToken);

        await serie.ApplyTo(oldSerie, serieRepository.DbContext);
        await serieRepository.Update(oldSerie, cancellationToken);

        await sender.SendAsync(nameof(SerieSubscriptions.SerieUpdated), oldSerie, cancellationToken);
        return oldSerie;
    }

    [Error<InvalidOperationException>]
    public static async Task<Serie> DeleteSerie([ID] Guid id, [Service] ISerieRepository serieRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie serie = await serieRepository.GetById(id, cancellationToken);

        await serieRepository.Remove(serie, cancellationToken);

        await sender.SendAsync(nameof(SerieSubscriptions.SerieDeleted), serie, cancellationToken);
        return serie;
    }

    // addSerieAuteur(metier: Metier!, personneId: UUID!, serieId: UUID!): Serie!
    // removeSerieAuteur(metier: Metier!, personneId: UUID!, serieId: UUID!): Serie!
    // addSerieGenre(genre: String!, serieId: UUID!): Serie!
    // removeSerieGenre(genre: String!, serieId: UUID!): Serie!
}
