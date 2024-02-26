namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<GenreAlbum> entity)
    {
        entity.ToTable("genres_albums");

        SetupVersioning(entity);

        entity.HasKey(
            e => new
            {
                e.AlbumId,
                e.GenreId
            }
        );

        entity.Property(e => e.FromSerie).HasDefaultValue(false);

        entity.HasOne(d => d.Album).WithMany(p => p.GenresAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Genre).WithMany(p => p.GenresAlbums).HasForeignKey(d => d.GenreId);
    }
}
