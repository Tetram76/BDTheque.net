namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<UniversAlbum> entity)
    {
        entity.ToTable("univers_albums");

        SetupVersioning(entity);

        entity.HasKey(
            e => new
            {
                e.AlbumId,
                e.UniversId
            }
        );

        entity.Property(e => e.FromSerie).HasDefaultValue(false);

        entity.HasOne(d => d.Album).WithMany(p => p.UniversAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Univers).WithMany(p => p.UniversAlbums).HasForeignKey(d => d.UniversId);
    }
}
