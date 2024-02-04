namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;

public abstract class MandatoryLabelType<T> : AssociableType<T>
    where T : MandatoryLabelEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        descriptor.Implements<MandatoryLabelInterface>();
        base.Configure(descriptor);
    }
}
