namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Scalars;

public class AlbumType : OptionalLabelType<Album>
{
    protected override void Configure(IObjectTypeDescriptor<Album> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Ignore(e => e.TitreRaw)
            .Ignore(e => e.SujetRaw)
            .Ignore(e => e.NotesRaw);

        descriptor.Field(o => o.Tome).Type<UnsignedShortType>();
        descriptor.Field(o => o.TomeDebut).Type<UnsignedShortType>();
        descriptor.Field(o => o.TomeFin).Type<UnsignedShortType>();

        descriptor.Field(o => o.MoisParution).Type<MonthType>();
        descriptor.Field(o => o.AnneeParution).Type<YearType>();

        descriptor.Ignore(e => e.SerieId).Field(o => o.Serie).Type<SerieType>();
        descriptor.Ignore(e => e.NotationId).Field(o => o.Notation).Type<OptionType>();

        descriptor
            .Ignore(e => e.AuteursAlbums)
            .Ignore(e => e.EditionsAlbums)
            .Ignore(e => e.GenresAlbums)
            .Ignore(e => e.UniversAlbums);
    }
}
