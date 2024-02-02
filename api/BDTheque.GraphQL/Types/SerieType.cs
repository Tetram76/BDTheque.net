namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

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

        descriptor.Field(e => e.Albums).Type<ListType<AlbumType>>()
            .UsePaging<AuteurType>().UseProjection<AuteurType>().UseFiltering<AuteurType>().UseSorting<AuteurType>();

        descriptor.Ignore(e => e.AuteursSeries)
            .Field("auteurs").Type<ListType<AuteurType>>()
            .Resolve(context => context.Parent<Serie>().AuteursSeries.Select(auteurSerie => auteurSerie.Auteur))
            .UsePaging<AuteurType>().UseProjection<AuteurType>().UseFiltering<AuteurType>().UseSorting<AuteurType>();

        descriptor.Ignore(e => e.GenresSeries)
            .Field("genres").Type<ListType<GenreType>>()
            .Resolve(context => context.Parent<Serie>().GenresSeries.Select(auteurSerie => auteurSerie.Genre))
            .UsePaging<GenreType>().UseProjection<GenreType>().UseFiltering<GenreType>().UseSorting<GenreType>();

        descriptor.Ignore(e => e.UniversSeries)
            .Field("univers").Type<ListType<UniversType>>()
            .Resolve(context => context.Parent<Serie>().UniversSeries.Select(universSerie => universSerie.Univers))
            .UsePaging<UniversType>().UseProjection<UniversType>().UseFiltering<UniversType>().UseSorting<UniversType>();
    }
}
