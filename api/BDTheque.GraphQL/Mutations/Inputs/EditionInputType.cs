namespace BDTheque.GraphQL.Mutations.Inputs;

public class EditionInputType : UniqueIdInputType<Edition>
{
    protected override void Configure(IInputObjectTypeDescriptor<Edition> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(e => e.Isbn).Type<IsbnType>();
    }
}
