namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
public static class AlbumMutations
{
    // createAlbum(data: AlbumCreateInput!): Album!
    // updateAlbum(data: AlbumUpdateInput!): Album!

    [Error<NotFoundIdException>]
    public static async Task<Album> DeleteAlbum([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album? album = await dbContext.Albums.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (album is null)
            throw new NotFoundIdException();
        dbContext.Albums.Remove(album);
        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumDeleted), album, cancellationToken);
        return album;
    }

    // addAlbumAuteur(albumId: UUID!, metier: Metier!, personneId: UUID!): Album!
    // removeAlbumAuteur(albumId: UUID!, metier: Metier!, personneId: UUID!): Album!
    // addAlbumGenre(albumId: UUID!, genre: String!): Album!
    // removeAlbumGenre(albumId: UUID!, genre: String!): Album!
}
