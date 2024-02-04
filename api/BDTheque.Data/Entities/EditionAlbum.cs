namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<EditionAlbum> entity)
    {
        entity.ToTable("editions_albums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.AlbumId,
                e.Id
            }
        ).IsUnique();

        entity.Property(e => e.Dedicace).HasDefaultValue(false);
        entity.Property(e => e.Gratuit).HasDefaultValue(false);
        entity.Property(e => e.Occasion).HasDefaultValue(false);
        entity.Property(e => e.Offert).HasDefaultValue(false);
        entity.Property(e => e.Prix).HasPrecision(8, 3);
        entity.Property(e => e.Stock).HasDefaultValue(true);

        entity.HasOne(d => d.Album).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Editeur).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.EditeurId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Edition).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.EditionId);
        entity.HasOne(d => d.Etat).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.EtatId).OnDelete(DeleteBehavior.SetNull);
        entity.HasOne(d => d.Collection).WithMany(p => p.EditionsAlbums)
            .HasPrincipalKey(
                p => new
                {
                    p.EditeurId,
                    p.Id
                }
            )
            .HasForeignKey(
                d => new
                {
                    d.EditeurId,
                    d.CollectionId
                }
            );
    }
}
