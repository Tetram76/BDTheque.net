namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;

public abstract class SimpleIdType<T> : VersioningType<T>
    where T : SimpleIdEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        descriptor.Implements<SimpleIdInterface>();
        base.Configure(descriptor);
    }
}
