namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Album>]
public static partial class AlbumMutations
{
    [Error<ValidationException>]
    public static async Task<Album> CreateAlbum(AlbumCreateInput album, [Service] IAlbumRepository albumRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album newAlbum = await album.ApplyTo(new Album(), albumRepository.DbContext);
        await albumRepository.Add(newAlbum, cancellationToken);

        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumCreated), newAlbum, cancellationToken);
        return newAlbum;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Album> UpdateAlbum(AlbumUpdateInput album, [Service] IAlbumRepository albumRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album oldAlbum = await albumRepository.GetById(album.Id, cancellationToken);

        await album.ApplyTo(oldAlbum, albumRepository.DbContext);
        await albumRepository.Update(oldAlbum, cancellationToken);

        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumUpdated), oldAlbum, cancellationToken);
        return oldAlbum;
    }

    [Error<InvalidOperationException>]
    public static async Task<Album> DeleteAlbum([ID] Guid id, [Service] IAlbumRepository albumRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album album = await albumRepository.GetById(id, cancellationToken);

        await albumRepository.Remove(album, cancellationToken);

        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumDeleted), album, cancellationToken);
        return album;
    }

    // addAlbumAuteur(albumId: UUID!, metier: Metier!, personneId: UUID!): Album!
    // removeAlbumAuteur(albumId: UUID!, metier: Metier!, personneId: UUID!): Album!
    // addAlbumGenre(albumId: UUID!, genre: String!): Album!
    // removeAlbumGenre(albumId: UUID!, genre: String!): Album!
}
