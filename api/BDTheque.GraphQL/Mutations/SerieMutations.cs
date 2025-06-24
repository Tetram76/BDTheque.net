namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;

using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Serie>]
public static partial class SerieMutations
{
    [Error<ValidationException>]
    public static async Task<Serie> CreateSerie(SerieCreateInput serie, [Service] ISerieRepository serieRepository, [Service] IServiceProvider serviceProvider, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie newSerie = await serie.ApplyTo(new Serie(), serviceProvider, cancellationToken);
        await serieRepository.Add(newSerie, cancellationToken);

        await sender.SendAsync(nameof(SerieSubscriptions.SerieCreatedStream), newSerie.Id, cancellationToken);
        return newSerie;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Serie> UpdateSerie(SerieUpdateInput serie, [Service] ISerieRepository serieRepository, [Service] IServiceProvider serviceProvider, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie oldSerie = await serieRepository.GetById(serie.Id, cancellationToken);

        await serie.ApplyTo(oldSerie, serviceProvider, cancellationToken);
        await serieRepository.Update(oldSerie, cancellationToken);

        await sender.SendAsync(nameof(SerieSubscriptions.SerieUpdatedStream), oldSerie.Id, cancellationToken);
        return oldSerie;
    }

    [Error<InvalidOperationException>]
    public static async Task<Serie> DeleteSerie([ID] Guid id, [Service] ISerieRepository serieRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Serie serie = await serieRepository.GetById(id, cancellationToken);

        await serieRepository.Remove(serie, cancellationToken);

        await sender.SendAsync(nameof(SerieSubscriptions.SerieDeletedStream), serie.Id, cancellationToken);
        return serie;
    }

    // addSerieAuteur(metier: Metier!, personneId: UUID!, serieId: UUID!): Serie!
    // removeSerieAuteur(metier: Metier!, personneId: UUID!, serieId: UUID!): Serie!
    // addSerieGenre(genre: String!, serieId: UUID!): Serie!
    // removeSerieGenre(genre: String!, serieId: UUID!): Serie!
}
