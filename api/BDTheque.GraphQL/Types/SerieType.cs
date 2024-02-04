namespace BDTheque.GraphQL.Types;

public class SerieType : OptionalLabelType<Serie>
{
    protected override void Configure(IObjectTypeDescriptor<Serie> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Ignore(e => e.TitreRaw)
            .Ignore(e => e.SujetRaw)
            .Ignore(e => e.NotesRaw);

        descriptor.Field(e => e.SiteWeb).Type<UrlType>();

        descriptor.Field(e => e.NbAlbums).Type<UnsignedShortType>();

        descriptor.Ignore(e => e.EditeurId).Field(e => e.Editeur).Type<EditeurType>();
        descriptor.Ignore(e => e.CollectionId).Field(e => e.Collection).Type<CollectionType>();

        descriptor.Ignore(e => e.ModeleEditionId).Field(e => e.ModeleEdition).Type<EditionType>();

        descriptor.Ignore(e => e.NotationId).Field(e => e.Notation).Type<OptionType>();

        descriptor
            .Ignore(e => e.Albums)
            .Ignore(e => e.AuteursSeries)
            .Ignore(e => e.GenresSeries)
            .Ignore(e => e.UniversSeries);
    }
}
