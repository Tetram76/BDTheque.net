namespace BDTheque.Model.Attributes;

using BDTheque.Model.Extensions;

using HotChocolate.Types.Descriptors;

public class ApplyObjectMappingAttribute : ObjectTypeDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IObjectTypeDescriptor descriptor, Type type)
        => descriptor.SetupDefaults(type);
}
