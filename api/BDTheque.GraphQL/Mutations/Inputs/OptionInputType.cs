namespace BDTheque.GraphQL.Mutations.Inputs;

public class OptionInputType : SimpleIdInputType<Option>
{
    protected override void Configure(IInputObjectTypeDescriptor<Option> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(e => e.Category).ID();
    }
}
