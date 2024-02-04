namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class SimpleIdInterface<T> : BaseInterface<T>
    where T : IEntityWithId
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("SimpleId");
        descriptor.Field(e => e.Id).ID();
    }
}
