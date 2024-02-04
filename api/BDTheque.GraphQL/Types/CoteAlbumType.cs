namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Scalars;

public class CoteAlbumType : UniqueIdType<CoteAlbum>
{
    protected override void Configure(IObjectTypeDescriptor<CoteAlbum> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.EditionAlbumId).Field(e => e.EditionAlbum).Type<EditionAlbumType>();

        descriptor.Field(e => e.Annee).Type<YearType>();

        descriptor.Field(e => e.Prix).Type<EuroCurrencyType>();
    }
}
