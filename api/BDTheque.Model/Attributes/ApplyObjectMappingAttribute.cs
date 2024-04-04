namespace BDTheque.Model.Attributes;

using HotChocolate.Types.Descriptors;

[AttributeUsage(AttributeTargets.Class)]
public class ApplyObjectMappingAttribute : ObjectTypeDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IObjectTypeDescriptor descriptor, Type type)
        => descriptor.SetupDefaults(type);
}
