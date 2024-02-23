namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
using BDTheque.Extensions;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Serie> entity)
    {
        entity.ToTable("series");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.ModeleEditionId).IsUnique();
        entity.HasIndex(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.TitreRaw);

        entity.Property(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.TitreRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Titre).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.SetupOptionalInitiale(e => e.Titre);
        entity.Property(e => e.Sujet).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.SujetRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Sujet).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Notes).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NotesRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Notes).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Complete).HasDefaultValue(false);
        entity.Property(e => e.SuivreManquants).HasDefaultValue(true);
        entity.Property(e => e.SuivreSorties).HasDefaultValue(true);
        entity.Property(e => e.Terminee).HasDefaultValue(false);

        entity.HasOne(d => d.Editeur).WithMany(p => p.Series).HasForeignKey(d => d.EditeurId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.ModeleEdition).WithOne(p => p.Serie).HasForeignKey<Serie>(d => d.ModeleEditionId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Notation).WithMany(p => p.Series).HasForeignKey(d => d.NotationId).OnDelete(DeleteBehavior.SetNull);
        entity.HasOne(d => d.Collection).WithMany(p => p.Series)
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
            )
            .OnDelete(DeleteBehavior.SetNull);
    }
}
