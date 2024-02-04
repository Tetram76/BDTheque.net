namespace BDTheque.GraphQL.Resolvers;

using Path = System.IO.Path;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[ExtendObjectType<ImageType>]
public static class ImageResolvers
{
    public const string ImageDirectory = "./wwwroot/images";
    public const string ImageRoot = "http://localhost:5000/images";

    [GraphQLType<UrlType>]
    public static Uri? GetUrl([GraphQLType<ImageType>][Parent] Image image)
        => File.Exists(Path.Combine(ImageDirectory, $"{image.Id}.png")) ? new Uri($"{ImageRoot}/{image.Id}.png") : null;
}
