namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Exceptions;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.Model.Inputs;

using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Album>]
public static partial class AlbumMutations
{
    [Error<ValidationException>]
    public static async Task<Album> CreateAlbum(AlbumCreateInput album, [Service] IAlbumRepository albumRepository, [Service] IServiceProvider serviceProvider, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album newAlbum = await album.ApplyTo(new Album(), serviceProvider, cancellationToken);
        await albumRepository.Add(newAlbum, cancellationToken);

        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumCreatedStream), newAlbum.Id, cancellationToken);
        return newAlbum;
    }

    [Error<ValidationException>]
    [Error<InvalidOperationException>]
    public static async Task<Album> UpdateAlbum(AlbumUpdateInput album, [Service] IAlbumRepository albumRepository, [Service] IServiceProvider serviceProvider, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album oldAlbum = await albumRepository.GetById(album.Id, cancellationToken);

        await album.ApplyTo(oldAlbum, serviceProvider, cancellationToken);
        await albumRepository.Update(oldAlbum, cancellationToken);

        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumUpdatedStream), oldAlbum.Id, cancellationToken);
        return oldAlbum;
    }

    [Error<InvalidOperationException>]
    public static async Task<Album> DeleteAlbum([ID] Guid id, [Service] IAlbumRepository albumRepository, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Album album = await albumRepository.GetById(id, cancellationToken);

        await albumRepository.Remove(album, cancellationToken);

        await sender.SendAsync(nameof(AlbumSubscriptions.AlbumDeletedStream), album.Id, cancellationToken);
        return album;
    }

    // addAlbumAuteur(albumId: UUID!, metier: Metier!, personneId: UUID!): Album!
    // removeAlbumAuteur(albumId: UUID!, metier: Metier!, personneId: UUID!): Album!
    // addAlbumGenre(albumId: UUID!, genre: String!): Album!
    // removeAlbumGenre(albumId: UUID!, genre: String!): Album!
}
