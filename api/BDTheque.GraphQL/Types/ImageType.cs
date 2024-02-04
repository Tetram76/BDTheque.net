namespace BDTheque.GraphQL.Types;

public class ImageType : UniqueIdType<Image>
{
    protected override void Configure(IObjectTypeDescriptor<Image> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.Bytes);
    }
}
