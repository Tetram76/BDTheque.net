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
[MutationEntity<Album>]
public static partial class AlbumMutations
{
    [Error<AlreadyExistsException>]
    public static async Task<Album> CreateAlbum(AlbumCreateInput album, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album newAlbum = await album.ApplyTo(new Album(), dbContext);
        dbContext.Albums.Add(newAlbum);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumCreated), newAlbum, cancellationToken);
        return newAlbum;
    }

    [Error<AlreadyExistsException>]
    [Error<NotFoundIdException>]
    public static async Task<Album> UpdateAlbum(AlbumUpdateInput album, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album? oldAlbum = await dbContext.Albums.SingleOrDefaultAsync(p => p.Id == album.Id, cancellationToken);
        if (oldAlbum is null)
            throw new NotFoundIdException(album.Id);

        await album.ApplyTo(oldAlbum, dbContext);
        dbContext.Update(oldAlbum);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumUpdated), oldAlbum, cancellationToken);
        return oldAlbum;
    }

    [Error<NotFoundIdException>]
    public static async Task<Album> DeleteAlbum([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album? album = await dbContext.Albums.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (album is null)
            throw new NotFoundIdException(id);
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
