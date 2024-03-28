namespace BDTheque.Data.Entities;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Option> entity)
    {
        entity.ToTable("options");

        SetupVersioning(entity);

        entity.HasKey(e => e.Id);

        entity.HasIndex(
                e => new
                {
                    e.Category,
                    e.Libelle
                }
            )
            .IsUnique().UseCollation(null!, BDThequeContext.FrenchCollation);

        entity.HasIndex(
            e => new
            {
                e.Category,
                e.Ordre
            }
        ).IsUnique();

        entity.Property(e => e.Libelle).UseCollation(BDThequeContext.FrenchCollation);
    }
}
