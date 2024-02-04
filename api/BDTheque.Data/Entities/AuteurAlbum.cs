namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<AuteurAlbum> entity)
    {
        entity.ToTable("auteurs_albums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.AuteurId,
                e.AlbumId
            }
        ).IsUnique();

        entity.HasOne(d => d.Album).WithMany(p => p.AuteursAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Auteur).WithMany(p => p.AuteursAlbums).HasForeignKey(d => d.AuteurId);
    }
}
