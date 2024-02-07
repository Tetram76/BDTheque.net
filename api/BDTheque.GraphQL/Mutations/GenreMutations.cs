namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class GenreMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Genre> CreateGenre(Genre genre, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Genres.AnyAsync(g => g.Nom == genre.Nom, cancellationToken))
            throw new AlreadyExistsException();

        dbContext.Genres.Add(genre);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreCreated), genre, cancellationToken);
        return genre;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Genre> UpdateGenre([ID] Guid id, Genre genre, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre? oldGenre = await dbContext.Genres.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (oldGenre is null)
            throw new NotFoundIdException();
        if (await dbContext.Genres.AnyAsync(g => g.Id != oldGenre.Id && g.Nom == genre.Nom, cancellationToken))
            throw new AlreadyExistsException();

        // GenreInputType.ApplyUpdate(genre, oldGenre);
        dbContext.Update(oldGenre);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreUpdated), oldGenre, cancellationToken);
        return oldGenre;
    }

    [Error<NotFoundIdException>]
    public static async Task<Genre> DeleteGenre([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre? genre = await dbContext.Genres.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (genre is null)
            throw new NotFoundIdException();

        dbContext.Genres.Remove(genre);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreDeleted), genre, cancellationToken);
        return genre;
    }
}
