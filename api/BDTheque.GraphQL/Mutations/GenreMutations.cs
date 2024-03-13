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
[MutationEntity<Genre>]
public static partial class GenreMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Genre> CreateGenre(GenreCreateInput genre, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Genres.AnyAsync(g => g.Nom == genre.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Genre name \"{genre.Nom.Value}\" is already used");

        Genre newGenre = (genre as IGenreInputType).ApplyTo(new Genre());
        dbContext.Genres.Add(newGenre);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreCreated), newGenre, cancellationToken);
        return newGenre;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Genre> UpdateGenre(GenreUpdateInput genre, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre? oldGenre = await dbContext.Genres.SingleOrDefaultAsync(p => p.Id == genre.Id, cancellationToken);
        if (oldGenre is null)
            throw new NotFoundIdException(genre.Id);
        if (genre.Nom.HasValue && await dbContext.Genres.AnyAsync(g => g.Id != oldGenre.Id && g.Nom == genre.Nom.Value, cancellationToken))
            throw new AlreadyExistsException($"Genre name \"{genre.Nom.Value}\" is already used");

        (genre as IGenreInputType).ApplyTo(oldGenre);
        dbContext.Update(oldGenre);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreUpdated), oldGenre, cancellationToken);
        return oldGenre;
    }

    [Error<NotFoundIdException>]
    public static async Task<Genre> DeleteGenre([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre? genre = await dbContext.Genres.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (genre is null)
            throw new NotFoundIdException(id);

        dbContext.Genres.Remove(genre);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreDeleted), genre, cancellationToken);
        return genre;
    }
}
