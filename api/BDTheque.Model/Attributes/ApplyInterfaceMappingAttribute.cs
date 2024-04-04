namespace BDTheque.Model.Attributes;

using HotChocolate.Types.Descriptors;

[AttributeUsage(AttributeTargets.Class)]
public class ApplyInterfaceMappingAttribute : InterfaceTypeDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IInterfaceTypeDescriptor descriptor, Type type)
        => descriptor.SetupDefaults(type);
}
