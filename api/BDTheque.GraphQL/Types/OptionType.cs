namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class OptionType : SimpleIdType<Option>
{
    protected override void Configure(IObjectTypeDescriptor<Option> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(e => e.Category).Type<UnsignedShortType>();
        descriptor.Field(e => e.Ordre).Type<UnsignedShortType>();

        descriptor.Field(e => e.Albums).Type<ListType<AlbumType>>()
            .UsePaging<AlbumType>().UseProjection<AlbumType>().UseFiltering<AlbumType>().UseSorting<AlbumType>();

        descriptor.Field(e => e.Series).Type<ListType<SerieType>>()
            .UsePaging<SerieType>().UseProjection<SerieType>().UseFiltering<SerieType>().UseSorting<SerieType>();

        descriptor.Field(e => e.EditionFormatEditions).Type<ListType<EditionType>>()
            .UsePaging<EditionType>().UseProjection<EditionType>().UseFiltering<EditionType>().UseSorting<EditionType>();
        descriptor.Field(e => e.EditionOrientations).Type<ListType<EditionType>>()
            .UsePaging<EditionType>().UseProjection<EditionType>().UseFiltering<EditionType>().UseSorting<EditionType>();
        descriptor.Field(e => e.EditionReliures).Type<ListType<EditionType>>()
            .UsePaging<EditionType>().UseProjection<EditionType>().UseFiltering<EditionType>().UseSorting<EditionType>();
        descriptor.Field(e => e.EditionSensLectures).Type<ListType<EditionType>>()
            .UsePaging<EditionType>().UseProjection<EditionType>().UseFiltering<EditionType>().UseSorting<EditionType>();
        descriptor.Field(e => e.EditionTypeEditions).Type<ListType<EditionType>>()
            .UsePaging<EditionType>().UseProjection<EditionType>().UseFiltering<EditionType>().UseSorting<EditionType>();

        descriptor.Field(e => e.EditionsAlbums).Type<ListType<EditionAlbumType>>()
            .UsePaging<EditionAlbumType>().UseProjection<EditionAlbumType>().UseFiltering<EditionAlbumType>().UseSorting<EditionAlbumType>();

        descriptor.Field(e => e.Images).Type<ListType<ImageType>>()
            .UsePaging<ImageType>().UseProjection<ImageType>().UseFiltering<ImageType>().UseSorting<ImageType>();
    }
}
