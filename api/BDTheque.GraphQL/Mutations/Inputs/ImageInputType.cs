namespace BDTheque.GraphQL.Mutations.Inputs;

public class ImageInputType : UniqueIdInputType<Image>
{
    protected override void Configure(IInputObjectTypeDescriptor<Image> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.Bytes);
    }
}
