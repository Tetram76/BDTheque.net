namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;
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

        descriptor.Ignore(e => e.AuteursAlbums)
            .Field("auteurs").Type<ListType<AuteurType>>()
            .Resolve(context => context.Parent<Album>().AuteursAlbums.Select(auteurAlbum => auteurAlbum.Auteur))
            .UsePaging<AuteurType>().UseProjection<AuteurType>().UseFiltering<AuteurType>().UseSorting<AuteurType>();

        descriptor.Field(e => e.EditionsAlbums).Type<ListType<EditionAlbumType>>()
            .UsePaging<EditionAlbumType>().UseProjection<EditionAlbumType>().UseFiltering<EditionAlbumType>().UseSorting<EditionAlbumType>();

        descriptor.Ignore(e => e.GenresAlbums)
            .Field("genres").Type<ListType<GenreType>>()
            .Resolve(context => context.Parent<Album>().GenresAlbums.Select(genreAlbum => genreAlbum.Genre))
            .UsePaging<GenreType>().UseProjection<GenreType>().UseFiltering<GenreType>().UseSorting<GenreType>();

        descriptor.Ignore(e => e.UniversAlbums)
            .Field("univers").Type<ListType<UniversType>>()
            .Resolve(context => context.Parent<Album>().UniversAlbums.Select(universAlbum => universAlbum.Univers))
            .UsePaging<UniversType>().UseProjection<UniversType>().UseFiltering<UniversType>().UseSorting<UniversType>();
    }
}
