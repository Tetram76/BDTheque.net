namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;
using BDTheque.GraphQL.Scalars;

public class EditionAlbumType : UniqueIdType<EditionAlbum>
{
    protected override void Configure(IObjectTypeDescriptor<EditionAlbum> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.EditionId).Field(e => e.Edition).Type<EditionType>();
        descriptor.Ignore(e => e.AlbumId).Field(e => e.Album).Type<AlbumType>();

        descriptor.Ignore(e => e.EditeurId).Field(e => e.Editeur).Type<EditeurType>();
        descriptor.Ignore(e => e.CollectionId).Field(e => e.Collection).Type<CollectionType>();

        descriptor.Ignore(e => e.EtatId).Field(e => e.Etat).Type<OptionType>();

        descriptor.Field(e => e.Prix).Type<EuroCurrencyType>();

        descriptor.Field(e => e.CotesAlbums).Name("cotes").Type<ListType<CoteAlbumType>>()
            .UsePaging<CoteAlbumType>().UseProjection<CoteAlbumType>().UseFiltering<CoteAlbumType>().UseSorting<CoteAlbumType>();

        descriptor.Field(e => e.Images).Type<ListType<ImageType>>()
            .UsePaging<ImageType>().UseProjection<ImageType>().UseFiltering<ImageType>().UseSorting<ImageType>();
    }
}
