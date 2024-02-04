namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;

public abstract class OptionalLabelType<T> : AssociableType<T>
    where T : EntityWithOptionalLabel
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<OptionalLabelInterface>();

        descriptor.Field(e => e.Initiale).Type<StringType>();
    }
}
