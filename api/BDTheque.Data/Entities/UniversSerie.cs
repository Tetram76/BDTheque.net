namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<UniversSerie> entity)
    {
        entity.ToTable("univers_series");

        SetupVersioning(entity);

        entity.HasKey(
            e => new
            {
                e.SerieId,
                e.UniversId
            }
        );

        entity.HasOne(d => d.Serie).WithMany(p => p.UniversSeries).HasForeignKey(d => d.SerieId);
        entity.HasOne(d => d.Univers).WithMany(p => p.UniversSeries).HasForeignKey(d => d.UniversId);
    }
}
