namespace BDTheque.GraphQL.Mutations.Inputs;

public class CoteEditionInputType : UniqueIdInputType<CoteEdition>
{
    protected override void Configure(IInputObjectTypeDescriptor<CoteEdition> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("CoteInput");
    }
}
