namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class OptionalLabelInterface : InterfaceType<IOptionalLabel>
{
    protected override void Configure(IInterfaceTypeDescriptor<IOptionalLabel> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field(e => e.Initiale).Type<StringType>();
    }
}
