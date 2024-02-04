namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Editeur> entity)
    {
        entity.ToTable("Editeurs");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation).IsUnique();
        entity.HasIndex(e => e.NomRaw);

        entity.Property(e => e.Initiale).HasMaxLength(1).HasComputedColumnSql($"(upper({entity.GetColumnName(e => e.Nom).ToSnakeCase()}))::character(1)", true);
        entity.Property(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NomRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Nom).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
    }
}
