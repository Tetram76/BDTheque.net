namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Editeur> entity)
    {
        entity.ToTable("editeurs");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation).IsUnique();
        entity.HasIndex(e => e.NomRaw);

        entity.SetupMandatoryInitiale(e => e.Nom);
        entity.Property(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NomRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Nom).ToSnakeCase()} COLLATE \"{BDThequeContext.PredictiveFrenchCollation}\")", true);
    }
}
