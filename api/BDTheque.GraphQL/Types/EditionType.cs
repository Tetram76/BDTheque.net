namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class EditionType : UniqueIdType<Edition>
{
    protected override void Configure(IObjectTypeDescriptor<Edition> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.NotesRaw);

        descriptor.Field(e => e.AnneeEdition).Type<UnsignedShortType>();
        descriptor.Field(e => e.Isbn).Type<IsbnType>();
        descriptor.Field(e => e.NombreDePages).Type<UnsignedShortType>();

        descriptor.Ignore(e => e.ReliureId).Field(o => o.Reliure).Type<OptionType>();
        descriptor.Ignore(e => e.FormatEditionId).Field(o => o.FormatEdition).Type<OptionType>();
        descriptor.Ignore(e => e.TypeEditionId).Field(o => o.TypeEdition).Type<OptionType>();
        descriptor.Ignore(e => e.OrientationId).Field(o => o.Orientation).Type<OptionType>();
        descriptor.Ignore(e => e.SensLectureId).Field(o => o.SensLecture).Type<OptionType>();

        descriptor.Field(e => e.EditionsAlbums).Name("albums").Type<ListType<EditionAlbumType>>()
            .UsePaging<EditionAlbumType>().UseProjection<EditionAlbumType>().UseFiltering<EditionAlbumType>().UseSorting<EditionAlbumType>();

        descriptor.Field(o => o.Serie).Type<SerieType>();
    }
}
