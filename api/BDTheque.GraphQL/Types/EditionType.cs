namespace BDTheque.GraphQL.Types;

public class EditionType : UniqueIdType<Edition>
{
    protected override void Configure(IObjectTypeDescriptor<Edition> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(e => e.Isbn).Type<IsbnType>();
    }
}
