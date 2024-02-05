namespace BDTheque.GraphQL.Inputs;

public class UniversInputType : MandatoryLabelInputType<Univers>
{
    protected override void Configure(IInputObjectTypeDescriptor<Univers> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(e => e.UniversRacine).Name("racine");
        descriptor.Field(e => e.UniversParent).Name("parent");
    }
}
