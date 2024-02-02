namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class EditeurType : MandatoryLabelType<Editeur>
{
    protected override void Configure(IObjectTypeDescriptor<Editeur> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.NomRaw);
        descriptor.Field(e => e.SiteWeb).Type<UrlType>();

        descriptor.Field(e => e.Collections).Type<ListType<CollectionType>>()
            .UsePaging<CollectionType>().UseProjection<CollectionType>().UseFiltering<CollectionType>().UseSorting<CollectionType>();
        descriptor.Field(e => e.Series).Type<ListType<SerieType>>()
            .UsePaging<SerieType>().UseProjection<SerieType>().UseFiltering<SerieType>().UseSorting<SerieType>();
        descriptor.Field(e => e.EditionsAlbums).Type<ListType<EditionAlbumType>>()
            .UsePaging<EditionAlbumType>().UseProjection<EditionAlbumType>().UseFiltering<EditionAlbumType>().UseSorting<EditionAlbumType>();
    }
}
