namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public abstract class AssociableType<T> : UniqueIdType<T>
    where T : AssociableEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<AssociableInterface<AssociableEntity>>();
    }
}
