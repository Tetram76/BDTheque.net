namespace BDTheque.GraphQL.Inputs;

public class CoteEditionInputType : UniqueIdInputType<CoteEdition>
{
    protected override void Configure(IInputObjectTypeDescriptor<CoteEdition> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("CoteInput");
    }
}
