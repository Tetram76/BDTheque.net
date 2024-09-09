namespace BDTheque.GraphQL.Mutations;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Attributes;
using BDTheque.GraphQL.Exceptions;
using BDTheque.GraphQL.Subscriptions;

using HotChocolate.Subscriptions;

using Path = System.IO.Path;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[MutationType]
[MutationEntity<Image>]
public static partial class ImageMutations
{
    // createImage(data: ImageCreateInput!): Image!
    // updateImage(data: ImageUpdateInput!): Image!

    [Error<NotFoundIdException>]
    public static async Task<Image> DeleteImage([ID] Guid id, BDThequeContext dbContext, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        Image? image = await dbContext.Images.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (image is null)
            throw new NotFoundIdException(id);

        dbContext.Images.Remove(image);

        await dbContext.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(nameof(ImageSubscriptions.ImageDeletedStream), image.Id, cancellationToken);
        return image;
    }

    public const string ImageDirectory = "./wwwroot/images";

    public static async Task<Image> UploadImagePicture([ID] Guid id, [GraphQLType<UploadType>] IFile file, CancellationToken cancellationToken)
    {
        await using FileStream stream = File.Create(Path.Combine(ImageDirectory, $"{id}.png"));
        await file.CopyToAsync(stream, cancellationToken);
        return new Image
        {
            Id = id,
            Titre = "Jon Skeet"
        };
    }
}
