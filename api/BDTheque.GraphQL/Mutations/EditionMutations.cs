namespace BDTheque.GraphQL.Mutations;

using BDTheque.GraphQL.Attributes;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<EditionDetail>]
public static partial class EditionMutations
{
    // createEdition(data: EditionCreateInput!): Edition!
    // updateEdition(data: EditionUpdateInput!): Edition!

    // [Error<NotFoundIdException>]
    // public static async Task<EditionDetail> DeleteEdition([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    // {
    //     EditionDetail? edition = await dbContext.EditionsDetails.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    //     if (edition is null)
    //         throw new NotFoundIdException(id);
    //
    //     dbContext.EditionsDetails.Remove(edition);
    //
    //     await dbContext.SaveChangesAsync(cancellationToken);
    //     await sender.SendAsync(nameof(EditionSubscriptions.EditionDeletedStream), edition.Id, cancellationToken);
    //     return edition;
    // }

    // addorupdateCote(annee: Year!, editionId: UUID!, prix: Currency!): [Cote!]!
    // removeCote(annee: Year!, editionId: UUID!): [Cote!]!
}
