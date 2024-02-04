namespace BDTheque.GraphQL.Types;

public class UniversType : MandatoryLabelType<Univers>
{
    protected override void Configure(IObjectTypeDescriptor<Univers> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Ignore(e => e.NomRaw)
            .Ignore(e => e.DescriptionRaw);

        descriptor.Field(e => e.SiteWeb).Type<UrlType>();

        descriptor.Ignore(e => e.UniversRacineId).Field(e => e.UniversRacine).Type<UniversType>();
        descriptor.Ignore(e => e.UniversParentId).Field(e => e.UniversParent).Type<UniversType>();

        descriptor.Field(e => e.UniversBranches).Name("branche");

        descriptor
            .Ignore(e => e.UniversParents)
            .Ignore(e => e.UniversRacines)
            .Ignore(e => e.UniversAlbums)
            .Ignore(e => e.UniversSeries);
    }
}
