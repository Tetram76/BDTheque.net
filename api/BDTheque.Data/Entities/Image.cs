namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Image> entity)
    {
        entity.ToTable("images");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.EditionId,
                e.TypeId,
                e.Ordre
            }
        ).IsUnique();

        entity.Property(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);

        entity.HasOne(d => d.Edition).WithMany(p => p.Images).HasForeignKey(d => d.EditionId);
        entity.HasOne(d => d.Type).WithMany(p => p.Images).HasForeignKey(d => d.TypeId).OnDelete(DeleteBehavior.Restrict);
    }
}
