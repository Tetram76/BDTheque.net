namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;

public abstract class UniqueIdType<T> : VersioningType<T>
    where T : UniqueIdEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        descriptor.Implements<UniqueIdInterface>();
        base.Configure(descriptor);
    }
}
