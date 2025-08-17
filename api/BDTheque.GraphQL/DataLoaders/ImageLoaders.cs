namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class ImageLoaders
{
    [DataLoader]
    internal static async Task<Image> GetImageByIdAsync([ID] Guid id, IImageRepository imageRepository, CancellationToken cancellationToken) =>
        await imageRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static async Task<Option> GetImageType(Image image, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(image).Reference(c => c.Type).LoadAsync(cancellationToken);
        return image.Type;
    }

    [DataLoader]
    internal static async Task<Edition> GetImageEdition(Image image, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(image).Reference(c => c.Edition).LoadAsync(cancellationToken);
        return image.Edition;
    }
}
