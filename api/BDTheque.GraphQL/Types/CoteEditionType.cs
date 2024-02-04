namespace BDTheque.GraphQL.Types;

public class CoteEditionType : UniqueIdType<CoteEdition>
{
    protected override void Configure(IObjectTypeDescriptor<CoteEdition> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name("Cote");
    }
}
