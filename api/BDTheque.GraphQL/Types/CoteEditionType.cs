namespace BDTheque.GraphQL.Types;

using BDTheque.GraphQL.Scalars;

public class CoteEditionType : UniqueIdType<CoteEdition>
{
    protected override void Configure(IObjectTypeDescriptor<CoteEdition> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name("Cote");

        descriptor.Ignore(e => e.EditionAlbumId).Field(e => e.EditionAlbum).Type<EditionAlbumType>();

        descriptor.Field(e => e.Annee).Type<YearType>();

        descriptor.Field(e => e.Prix).Type<EuroCurrencyType>();
    }
}
