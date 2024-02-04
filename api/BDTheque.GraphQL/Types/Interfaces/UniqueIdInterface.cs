namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class UniqueIdInterface : InterfaceType<IUniqueId>
{
    protected override void Configure(IInterfaceTypeDescriptor<IUniqueId> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field(e => e.Id).ID();
    }
}
