namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public abstract class UniqueIdType<T> : BaseType<T>
    where T : EntityWithUniqueId
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<UniqueIdInterface<EntityWithUniqueId>>();

        descriptor.Field(e => e.Id).ID();
    }
}
