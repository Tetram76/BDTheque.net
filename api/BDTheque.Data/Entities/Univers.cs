namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
using BDTheque.Extensions;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Univers> entity)
    {
        entity.ToTable("univers");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.NomRaw);

        entity.Property(e => e.Description).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.DescriptionRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Description).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.SetupMandatoryInitiale(e => e.Nom);
        entity.Property(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NomRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Nom).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);

        entity.HasOne(d => d.UniversParent).WithMany(p => p.UniversParents).HasForeignKey(d => d.UniversParentId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.UniversRacine).WithMany(p => p.UniversRacines).HasForeignKey(d => d.UniversRacineId).OnDelete(DeleteBehavior.Restrict);
    }
}
