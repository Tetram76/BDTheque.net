namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
using BDTheque.Extensions;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Personne> entity)
    {
        entity.ToTable("personnes");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.Nom).IsUnique().UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.NomRaw);

        entity.Property(e => e.Biographie).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.BiographieRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Biographie).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.SetupMandatoryInitiale(e => e.Nom);
        entity.Property(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NomRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Nom).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
    }
}
