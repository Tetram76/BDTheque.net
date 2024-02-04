namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;

public abstract class AssociableType<T> : UniqueIdType<T>
    where T : AssociableEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        descriptor.Implements<AssociableInterface>();
        base.Configure(descriptor);
    }
}
