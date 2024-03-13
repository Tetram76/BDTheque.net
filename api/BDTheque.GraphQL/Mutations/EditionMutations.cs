namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using HotChocolate.Subscriptions;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Edition>]
public static partial class EditionMutations
{
    // createEditionAlbum(data: EditionAlbumCreateInput!): EditionAlbum!
    // updateEditionAlbum(data: EditionAlbumUpdateInput!): EditionAlbum!

    [Error<NotFoundIdException>]
    public static async Task<Edition> DeleteEdition([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Edition? edition = await dbContext.Editions.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (edition is null)
            throw new NotFoundIdException(id);

        dbContext.Editions.Remove(edition);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(EditionSubscriptions.EditionDeleted), edition, cancellationToken);
        return edition;
    }

    // addorupdateCote(annee: Year!, editionId: UUID!, prix: Currency!): [Cote!]!
    // removeCote(annee: Year!, editionId: UUID!): [Cote!]!
}
