namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Extensions;
using BDTheque.GraphQL.Types.Interfaces;
using BDTheque.Model.Entities.Abstract;

public abstract class VersioningType<T> : ObjectType<T>
    where T : VersioningEntity
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        descriptor.Implements<VersioningInterface>();
        base.Configure(descriptor);

        descriptor.SetupDefaults();
    }

}
