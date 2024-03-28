namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<GenreSerie> entity)
    {
        entity.ToTable("genres_series");

        SetupVersioning(entity);

        entity.HasKey(
            e => new
            {
                e.SerieId,
                e.GenreId
            }
        );

        entity.HasOne(d => d.Genre).WithMany(p => p.GenresSeries).HasForeignKey(d => d.GenreId);
        entity.HasOne(d => d.Serie).WithMany(p => p.GenresSeries).HasForeignKey(d => d.SerieId);
    }
}
