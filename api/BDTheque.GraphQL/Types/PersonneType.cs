namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class PersonneType : MandatoryLabelType<Personne>
{
    protected override void Configure(IObjectTypeDescriptor<Personne> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.Auteurs);

        descriptor
            .Field("albums").Type<ListType<AlbumType>>()
            .Resolve(context => context.Parent<Personne>().Auteurs.SelectMany(auteur => auteur.AuteursAlbums).Select(auteurAlbum => auteurAlbum.Album))
            .UsePaging<AlbumType>().UseProjection<AlbumType>().UseFiltering<AlbumType>().UseSorting<AlbumType>();

        descriptor
            .Field("series").Type<ListType<SerieType>>()
            .Resolve(context => context.Parent<Personne>().Auteurs.SelectMany(auteur => auteur.AuteursSeries).Select(auteurSerie => auteurSerie.Serie))
            .UsePaging<SerieType>().UseProjection<SerieType>().UseFiltering<SerieType>().UseSorting<SerieType>();
    }
}
