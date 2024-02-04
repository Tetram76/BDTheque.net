namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<CoteAlbum> entity)
    {
        entity.ToTable("CotesAlbums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.EditionAlbumId,
                e.Annee
            }
        ).IsUnique();

        entity.Property(e => e.Prix).HasPrecision(8, 3);

        entity.HasOne(d => d.EditionAlbum).WithMany(p => p.CotesAlbums).HasForeignKey(d => d.EditionAlbumId);
    }
}
