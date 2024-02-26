namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
using BDTheque.Extensions;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Collection> entity)
    {
        entity.ToTable("collections");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.EditeurId,
                e.Id
            }
        ).IsUnique();

        entity.HasIndex(
                e => new
                {
                    e.EditeurId,
                    e.Nom
                }
            )
            .IsUnique()
            .UseCollation(null!, BDThequeContext.FrenchCollation);

        entity.HasIndex(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.NomRaw);

        entity.SetupMandatoryInitiale(e => e.Nom);
        entity.Property(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NomRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Nom).ToSnakeCase()} COLLATE \"{BDThequeContext.PredictiveFrenchCollation}\")", true);

        entity.HasOne(d => d.Editeur).WithMany(p => p.Collections).HasForeignKey(d => d.EditeurId);
    }
}
