namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Interfaces;

public abstract class AssociableType<T> : UniqueIdType<T>
    where T : AssociableEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<AssociableInterface<IAssociableEntity>>();
    }
}
