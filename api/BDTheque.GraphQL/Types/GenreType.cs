namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class GenreType : MandatoryLabelType<Genre>
{
    protected override void Configure(IObjectTypeDescriptor<Genre> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.NomRaw);

        descriptor.Ignore(e => e.GenresAlbums)
            .Field("albums").Type<ListType<AlbumType>>()
            .Resolve(context => context.Parent<Genre>().GenresAlbums.Select(genreAlbum => genreAlbum.Album))
            .UsePaging<AlbumType>().UseProjection<AlbumType>().UseFiltering<AlbumType>().UseSorting<AlbumType>();

        descriptor.Ignore(e => e.GenresSeries)
            .Field("series").Type<ListType<SerieType>>()
            .Resolve(context => context.Parent<Genre>().GenresSeries.Select(genreSerie => genreSerie.Serie))
            .UsePaging<SerieType>().UseProjection<SerieType>().UseFiltering<SerieType>().UseSorting<SerieType>();
    }
}
