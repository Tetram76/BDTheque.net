namespace BDTheque.Model.Attributes;

using HotChocolate.Types.Descriptors;

public class ApplyMutationInputMappingAttribute : InputObjectTypeDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IInputObjectTypeDescriptor descriptor, Type type)
        => descriptor.SetupDefaults(type);
}
