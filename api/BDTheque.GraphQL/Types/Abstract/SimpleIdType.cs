namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Interfaces;

public abstract class SimpleIdType<T> : BaseType<T>
    where T : EntityWithId
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<SimpleIdInterface<IEntityWithId>>();

        descriptor.Field(e => e.Id).ID();
    }
}
