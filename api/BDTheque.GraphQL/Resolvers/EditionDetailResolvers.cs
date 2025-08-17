namespace BDTheque.GraphQL.Resolvers;

using BDTheque.GraphQL.DataLoaders;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<EditionDetail>]
public static class EditionDetailResolvers
{
    public static async Task<Option?> GetEtat([Parent] EditionDetail editionDetail, IEditionDetailEtatDataLoader loader, CancellationToken cancellationToken) =>
        editionDetail.Etat ??= await loader.LoadAsync(editionDetail, cancellationToken);

    public static async Task<Option?> GetReliure([Parent] EditionDetail editionDetail, IEditionDetailReliureDataLoader loader, CancellationToken cancellationToken) =>
        editionDetail.Reliure ??= await loader.LoadAsync(editionDetail, cancellationToken);

    public static async Task<Option?> GetFormatEdition([Parent] EditionDetail editionDetail, IEditionDetailFormatEditionDataLoader loader, CancellationToken cancellationToken) =>
        editionDetail.FormatEdition ??= await loader.LoadAsync(editionDetail, cancellationToken);

    public static async Task<Option?> GetTypeEdition([Parent] EditionDetail editionDetail, IEditionDetailTypeEditionDataLoader loader, CancellationToken cancellationToken) =>
        editionDetail.TypeEdition ??= await loader.LoadAsync(editionDetail, cancellationToken);

    public static async Task<Option?> GetOrientation([Parent] EditionDetail editionDetail, IEditionDetailOrientationDataLoader loader, CancellationToken cancellationToken) =>
        editionDetail.Orientation ??= await loader.LoadAsync(editionDetail, cancellationToken);

    public static async Task<Option?> GetSensLecture([Parent] EditionDetail editionDetail, IEditionDetailSensLectureDataLoader loader, CancellationToken cancellationToken) =>
        editionDetail.SensLecture ??= await loader.LoadAsync(editionDetail, cancellationToken);
}
