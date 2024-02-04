namespace BDTheque.GraphQL.Types;

public class OptionType : SimpleIdType<Option>
{
    protected override void Configure(IObjectTypeDescriptor<Option> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(e => e.Category).Type<UnsignedShortType>();
        descriptor.Field(e => e.Ordre).Type<UnsignedShortType>();

        descriptor
            .Ignore(e => e.Albums)
            .Ignore(e => e.Series)
            .Ignore(e => e.EditionFormatEditions)
            .Ignore(e => e.EditionOrientations)
            .Ignore(e => e.EditionReliures)
            .Ignore(e => e.EditionSensLectures)
            .Ignore(e => e.EditionTypeEditions)
            .Ignore(e => e.EditionsAlbums)
            .Ignore(e => e.Images);
    }
}
