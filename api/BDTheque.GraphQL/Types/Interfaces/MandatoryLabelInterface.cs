namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class MandatoryLabelInterface<T> : AssociableInterface<T>
    where T : IEntityWithLabel
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("MandatoryLabel");
        descriptor.Field(e => e.Initiale).Type<NonNullType<StringType>>();
    }
}
