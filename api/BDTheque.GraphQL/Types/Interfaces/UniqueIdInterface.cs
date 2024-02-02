namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class UniqueIdInterface<T> : BaseInterface<T>
    where T : EntityWithUniqueId
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("UniqueId");
        descriptor.Field(e => e.Id).ID();
    }
}
