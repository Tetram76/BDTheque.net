namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

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

        descriptor.Field(e => e.UniversRacines).Name("racines").Type<ListType<UniversType>>();
        descriptor.Field(e => e.UniversParents).Name("parents").Type<ListType<UniversType>>();

        descriptor.Ignore(e => e.UniversAlbums)
            .Field("albums").Type<ListType<AlbumType>>()
            .Resolve(context => context.Parent<Univers>().UniversAlbums.Select(genreAlbum => genreAlbum.Album))
            .UsePaging<AlbumType>().UseProjection<AlbumType>().UseFiltering<AlbumType>().UseSorting<AlbumType>();

        descriptor.Ignore(e => e.UniversSeries)
            .Field("series").Type<ListType<SerieType>>()
            .Resolve(context => context.Parent<Univers>().UniversSeries.Select(genreSerie => genreSerie.Serie))
            .UsePaging<SerieType>().UseProjection<SerieType>().UseFiltering<SerieType>().UseSorting<SerieType>();
    }
}
