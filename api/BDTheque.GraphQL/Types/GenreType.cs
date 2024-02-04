namespace BDTheque.GraphQL.Types;

public class GenreType : MandatoryLabelType<Genre>
{
    protected override void Configure(IObjectTypeDescriptor<Genre> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(e => e.NomRaw);

        descriptor
            .Ignore(e => e.GenresAlbums)
            .Ignore(e => e.GenresSeries);
    }
}
