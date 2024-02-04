namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class MandatoryLabelInterface : InterfaceType<IMandatoryLabel>
{
    protected override void Configure(IInterfaceTypeDescriptor<IMandatoryLabel> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field(e => e.Initiale).Type<NonNullType<StringType>>();
    }
}
