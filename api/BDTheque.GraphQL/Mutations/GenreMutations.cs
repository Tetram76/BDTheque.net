namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;

using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Genre>]
public static partial class GenreMutations
{
    [Error<ValidationException>]
    public static async Task<Genre> CreateGenre(GenreCreateInput genre, [Service] IGenreRepository genreRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre newGenre = (genre as IGenreInputType).ApplyTo(new Genre());
        await genreRepository.Add(newGenre, cancellationToken);

        await sender.SendAsync(nameof(GenreSubscriptions.GenreCreatedStream), newGenre.Id, cancellationToken);
        return newGenre;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Genre> UpdateGenre(GenreUpdateInput genre, [Service] IGenreRepository genreRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre oldGenre = await genreRepository.GetById(genre.Id, cancellationToken);

        (genre as IGenreInputType).ApplyTo(oldGenre);
        await genreRepository.Update(oldGenre, cancellationToken);

        await sender.SendAsync(nameof(GenreSubscriptions.GenreUpdatedStream), oldGenre.Id, cancellationToken);
        return oldGenre;
    }

    [Error<InvalidOperationException>]
    public static async Task<Genre> DeleteGenre([ID] Guid id, [Service] IGenreRepository genreRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre genre = await genreRepository.GetById(id, cancellationToken);

        await genreRepository.Remove(genre, cancellationToken);

        await sender.SendAsync(nameof(GenreSubscriptions.GenreDeletedStream), genre.Id, cancellationToken);
        return genre;
    }
}
