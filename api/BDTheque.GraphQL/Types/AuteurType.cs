namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class AuteurType : UniqueIdType<Auteur>
{
    protected override void Configure(IObjectTypeDescriptor<Auteur> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.PersonneId).Field(e => e.Personne).Type<PersonneType>();

        descriptor.Ignore(e => e.AuteursAlbums)
            .Field("albums").Type<ListType<AlbumType>>()
            .Resolve(context => context.Parent<Auteur>().AuteursAlbums.Select(auteurAlbum => auteurAlbum.Album))
            .UsePaging<AlbumType>().UseProjection<AlbumType>().UseFiltering<AlbumType>().UseSorting<AlbumType>();

        descriptor.Ignore(e => e.AuteursSeries)
            .Field("series").Type<ListType<SerieType>>()
            .Resolve(context => context.Parent<Auteur>().AuteursSeries.Select(auteurSerie => auteurSerie.Serie))
            .UsePaging<SerieType>().UseProjection<SerieType>().UseFiltering<SerieType>().UseSorting<SerieType>();
    }
}
