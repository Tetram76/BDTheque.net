namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class OptionalLabelInterface<T> : AssociableInterface<T>
    where T : IEntityWithOptionalLabel
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("OptionalLabel");
        descriptor.Field(e => e.Initiale).Type<StringType>();
    }
}
