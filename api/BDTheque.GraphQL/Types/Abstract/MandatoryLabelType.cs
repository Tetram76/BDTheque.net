namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Interfaces;

public abstract class MandatoryLabelType<T> : AssociableType<T>
    where T : EntityWithLabel
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Implements<MandatoryLabelInterface<IEntityWithLabel>>();

        descriptor.Field(e => e.Initiale).Type<NonNullType<StringType>>();
    }
}
