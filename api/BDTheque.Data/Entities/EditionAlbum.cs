namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
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

        entity.HasAlternateKey(
            e => new
            {
                e.AlbumId,
                e.EditionId
            }
        );

        entity.Property(e => e.Notes).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NotesRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Notes).ToSnakeCase()} COLLATE \"{BDThequeContext.PredictiveFrenchCollation}\")", true);
        entity.Property(e => e.Dedicace).HasDefaultValue(false);
        entity.Property(e => e.Gratuit).HasDefaultValue(false);
        entity.Property(e => e.Occasion).HasDefaultValue(false);
        entity.Property(e => e.Offert).HasDefaultValue(false);
        entity.Property(e => e.Stock).HasDefaultValue(true);

        entity.HasOne(d => d.Album).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Editeur).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.EditeurId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Edition).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.EditionId);
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
