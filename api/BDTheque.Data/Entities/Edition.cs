namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Edition> entity)
    {
        entity.ToTable("Editions");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.Property(e => e.Notes).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NotesRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Notes).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Couleur).HasDefaultValue(true);
        entity.Property(e => e.Vo).HasDefaultValue(false);

        entity.HasOne(d => d.FormatEdition).WithMany(p => p.EditionFormatEditions).HasForeignKey(d => d.FormatEditionId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Orientation).WithMany(p => p.EditionOrientations).HasForeignKey(d => d.OrientationId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Reliure).WithMany(p => p.EditionReliures).HasForeignKey(d => d.ReliureId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.SensLecture).WithMany(p => p.EditionSensLectures).HasForeignKey(d => d.SensLectureId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.TypeEdition).WithMany(p => p.EditionTypeEditions).HasForeignKey(d => d.TypeEditionId).OnDelete(DeleteBehavior.Restrict);
    }
}
