namespace BDTheque.Model.Attributes;

using BDTheque.Model.Extensions;

using HotChocolate.Types.Descriptors;

public class ApplyInterfaceMappingAttribute : InterfaceTypeDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IInterfaceTypeDescriptor descriptor, Type type)
        => descriptor.SetupDefaults(type);
}
