namespace BDTheque.GraphQL.Types;

public class UniversType : MandatoryLabelType<Univers>
{
    protected override void Configure(IObjectTypeDescriptor<Univers> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(e => e.UniversRacine).Name("racine");
        descriptor.Field(e => e.UniversParent).Name("parent");
    }
}
