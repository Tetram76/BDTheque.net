namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class MandatoryLabelInterface<T> : AssociableInterface<T>
    where T : EntityWithLabel
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("MandatoryLabel");
        descriptor.Field(e => e.Initiale).Type<NonNullType<StringType>>();
    }
}
