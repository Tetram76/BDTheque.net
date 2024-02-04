namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class SimpleIdInterface : InterfaceType<ISimpleId>
{
    protected override void Configure(IInterfaceTypeDescriptor<ISimpleId> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field(e => e.Id).ID();
    }
}
