namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public abstract class MandatoryLabelType<T> : AssociableType<T>
    where T : EntityWithLabel
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<MandatoryLabelInterface<EntityWithLabel>>();

        descriptor.Field(e => e.Initiale).Type<NonNullType<StringType>>();
    }
}
