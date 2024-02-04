namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class UniqueIdInterface<T> : BaseInterface<T>
    where T : IEntityWithUniqueId
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("UniqueId");
        descriptor.Field(e => e.Id).ID();
    }
}
