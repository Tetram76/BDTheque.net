namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Album> entity)
    {
        entity.ToTable("albums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.TitreRaw);

        entity.SetupOptionalInitiale(e => e.Titre);
        entity.Property(e => e.Notes).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NotesRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Notes).ToSnakeCase()} COLLATE \"{BDThequeContext.PredictiveFrenchCollation}\")", true);
        entity.Property(e => e.Sujet).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.SujetRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Sujet).ToSnakeCase()} COLLATE \"{BDThequeContext.PredictiveFrenchCollation}\")", true);
        entity.Property(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.TitreRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Titre).ToSnakeCase()} COLLATE \"{BDThequeContext.PredictiveFrenchCollation}\")", true);
        entity.Property(e => e.HorsSerie).HasDefaultValue(false);
        entity.Property(e => e.Integrale).HasDefaultValue(false);

        entity.HasOne(d => d.Notation).WithMany(p => p.Albums).HasForeignKey(d => d.NotationId).OnDelete(DeleteBehavior.SetNull);
        entity.HasOne(d => d.Serie).WithMany(p => p.Albums).HasForeignKey(d => d.SerieId).OnDelete(DeleteBehavior.SetNull);
    }
}
