namespace BDTheque.GraphQL.Types;

public class CollectionType : MandatoryLabelType<Collection>
{
    protected override void Configure(IObjectTypeDescriptor<Collection> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.NomRaw);
        descriptor.Ignore(e => e.EditeurId).Field(e => e.Editeur).Type<EditeurType>();

        descriptor
            .Ignore(e => e.EditionsAlbums)
            .Ignore(e => e.Series);
    }
}
