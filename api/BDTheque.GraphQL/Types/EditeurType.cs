namespace BDTheque.GraphQL.Types;

public class EditeurType : MandatoryLabelType<Editeur>
{
    protected override void Configure(IObjectTypeDescriptor<Editeur> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.NomRaw);
        descriptor.Field(e => e.SiteWeb).Type<UrlType>();

        descriptor
            .Ignore(e => e.Collections)
            .Ignore(e => e.Series)
            .Ignore(e => e.EditionsAlbums);
    }
}
