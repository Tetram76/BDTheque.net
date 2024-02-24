namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class GenreMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Genre> CreateGenre(GenreCreateInput genre, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Genres.AnyAsync(g => g.Nom == genre.Nom, cancellationToken))
            throw new AlreadyExistsException();

        Genre newGenre = genre.BuildEntity<Genre>();
        dbContext.Genres.Add(newGenre);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreCreated), newGenre, cancellationToken);
        return newGenre;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Genre> UpdateGenre(GenreUpdateInput genre, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre? oldGenre = await dbContext.Genres.Where(p => p.Id == genre.Id).SingleOrDefaultAsync(cancellationToken);
        if (oldGenre is null)
            throw new NotFoundIdException();
        if (genre.Nom.HasValue && await dbContext.Genres.AnyAsync(g => g.Id != oldGenre.Id && g.Nom == genre.Nom, cancellationToken))
            throw new AlreadyExistsException();

        genre.ApplyUpdate(oldGenre);
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
