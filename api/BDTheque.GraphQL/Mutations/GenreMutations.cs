namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.GraphQL.Types;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[MutationType]
public static class GenreMutations
{
    [Error<AlreadyExistsException>]
    [GraphQLType<GenreType>]
    public static async Task<Genre> CreateGenre(string nom, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        if (await dbContext.Genres.AnyAsync(genre => genre.Nom == nom, cancellationToken))
            throw new AlreadyExistsException();

        var genre = new Genre
        {
            Nom = nom
        };
        dbContext.Genres.Add(genre);
        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreCreated), genre, cancellationToken);
        return genre;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    [GraphQLType<GenreType>]
    public static async Task<Genre> UpdateGenre([ID] Guid id, string nom, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Genre? genre = await dbContext.Genres.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (genre is null)
            throw new NotFoundIdException();
        if (await dbContext.Genres.AnyAsync(g => g.Id != genre.Id && g.Nom == nom, cancellationToken))
            throw new AlreadyExistsException();

        genre.Nom = nom;

        dbContext.Update(genre);
        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(GenreSubscriptions.GenreUpdated), genre, cancellationToken);
        return genre;
    }

    [Error<NotFoundIdException>]
    [GraphQLType<GenreType>]
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
