namespace BDTheque.GraphQL.Types;

public class OptionType : SimpleIdType<Option>
{
    protected override void Configure(IObjectTypeDescriptor<Option> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(e => e.Category).ID();
    }
}
