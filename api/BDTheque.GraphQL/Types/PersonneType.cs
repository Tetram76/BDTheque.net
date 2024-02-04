namespace BDTheque.GraphQL.Types;

public class PersonneType : MandatoryLabelType<Personne>
{
    protected override void Configure(IObjectTypeDescriptor<Personne> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.Auteurs);
    }
}
