namespace BDTheque.Model.Attributes;

using BDTheque.Model.Extensions;
using HotChocolate.Types.Descriptors;

public class ApplyDefaultObjectMappingAttribute : ObjectTypeDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IObjectTypeDescriptor descriptor, Type type)
        => descriptor.SetupDefaults(type);
}
