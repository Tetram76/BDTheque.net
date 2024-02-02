namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;
using BDTheque.GraphQL.Types;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

[MutationType]
public static class ImageMutations
{
    // createImage(data: ImageCreateInput!): Image!
    // updateImage(data: ImageUpdateInput!): Image!

    [Error<NotFoundIdException>]
    [GraphQLType<ImageType>]
    public static async Task<Image> DeleteImage([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Image? image = await dbContext.Images.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (image is null)
            throw new NotFoundIdException();
        dbContext.Images.Remove(image);
        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(ImageSubscriptions.ImageDeleted), image, cancellationToken);
        return image;
    }
}
