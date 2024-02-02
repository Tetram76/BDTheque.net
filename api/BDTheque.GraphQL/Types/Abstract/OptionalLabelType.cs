namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public abstract class OptionalLabelType<T> : AssociableType<T>
    where T : EntityWithOptionalLabel
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<OptionalLabelInterface<EntityWithOptionalLabel>>();

        descriptor.Field(e => e.Initiale).Type<StringType>();
    }
}
