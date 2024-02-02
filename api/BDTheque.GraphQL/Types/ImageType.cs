namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class ImageType : UniqueIdType<Image>
{
    protected override void Configure(IObjectTypeDescriptor<Image> descriptor)
    {
        base.Configure(descriptor);

        // public byte[] Bytes { get; set; } = null!;

        descriptor.Field(e => e.Ordre).Type<UnsignedShortType>();
        descriptor.Ignore(e => e.TypeId).Field(e => e.Type).Type<OptionType>();
        descriptor.Ignore(e => e.EditionId).Field(e => e.Edition).Type<EditionAlbumType>();
    }
}
