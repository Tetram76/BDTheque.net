namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;

public abstract class UniqueIdType<T> : BaseType<T>
    where T : EntityWithUniqueId
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<UniqueIdInterface>();

        descriptor.Field(e => e.Id).ID();
    }
}
