namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class SimpleIdInterface<T> : BaseInterface<T>
    where T : EntityWithId
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("SimpleId");
        descriptor.Field(e => e.Id).ID();
    }
}
