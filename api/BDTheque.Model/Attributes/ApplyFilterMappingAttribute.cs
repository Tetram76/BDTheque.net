namespace BDTheque.Model.Attributes;

using HotChocolate.Data.Filters;
using HotChocolate.Types.Descriptors;

public class ApplyFilterInputMappingAttribute : FilterInputTypeDescriptorAttribute
{
    public override void OnConfigure(IDescriptorContext context, IFilterInputTypeDescriptor descriptor, Type type)
        => descriptor.SetupDefaults(type);
}
