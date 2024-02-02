namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class CollectionType : MandatoryLabelType<Collection>
{
    protected override void Configure(IObjectTypeDescriptor<Collection> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.NomRaw);
        descriptor.Ignore(e => e.EditeurId).Field(e => e.Editeur).Type<EditeurType>();

        descriptor.Field(e => e.EditionsAlbums).Name("albums").Type<ListType<EditionAlbumType>>()
            .UsePaging<EditionAlbumType>().UseProjection<EditionAlbumType>().UseFiltering<EditionAlbumType>().UseSorting<EditionAlbumType>();
        descriptor.Field(e => e.Series).Type<ListType<SerieType>>()
            .UsePaging<SerieType>().UseProjection<SerieType>().UseFiltering<SerieType>().UseSorting<SerieType>();
    }
}
