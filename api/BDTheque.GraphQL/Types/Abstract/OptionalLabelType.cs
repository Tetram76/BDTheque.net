namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;

public abstract class OptionalLabelType<T> : AssociableType<T>
    where T : OptionalLabelEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        descriptor.Implements<OptionalLabelInterface>();
        base.Configure(descriptor);
    }
}
