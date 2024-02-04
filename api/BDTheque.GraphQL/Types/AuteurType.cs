namespace BDTheque.GraphQL.Types;

public class AuteurType : UniqueIdType<Auteur>
{
    protected override void Configure(IObjectTypeDescriptor<Auteur> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.PersonneId).Field(e => e.Personne).Type<PersonneType>();

        descriptor
            .Ignore(e => e.AuteursAlbums)
            .Ignore(e => e.AuteursSeries);
    }
}
