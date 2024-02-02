namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public abstract class SimpleIdType<T> : BaseType<T>
    where T : EntityWithId
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<SimpleIdInterface<EntityWithId>>();

        descriptor.Field(e => e.Id).ID();
    }
}
