namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public abstract class BaseType<T> : ObjectType<T>
    where T : BaseEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<BaseInterface<BaseEntity>>();
    }
}
