namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Cote> entity)
    {
        entity.ToTable("cotes");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.EditionId,
                e.Annee
            }
        ).IsUnique();

        entity.Property(e => e.Prix).HasPrecision(8, 3);

        entity.HasOne(d => d.Edition).WithMany(p => p.Cotes).HasForeignKey(d => d.EditionId);
    }
}
