namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Interfaces;

public abstract class BaseType<T> : ObjectType<T>
    where T : BaseEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<BaseInterface<IBaseEntity>>();
    }
}
