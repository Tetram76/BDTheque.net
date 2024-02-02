namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class OptionalLabelInterface<T> : AssociableInterface<T>
    where T : EntityWithOptionalLabel
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("OptionalLabel");
        descriptor.Field(e => e.Initiale).Type<StringType>();
    }
}
