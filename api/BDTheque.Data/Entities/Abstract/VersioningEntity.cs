namespace BDTheque.Data.Entities;

using BDTheque.Model.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    private static void SetupVersioning<T>(EntityTypeBuilder<T> entity) where T : class, IVersioningEntity
    {
        entity.Property(e => e.CreatedAt).HasColumnType("timestamp(3) with time zone").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        entity.Property(e => e.UpdatedAt).HasColumnType("timestamp(3) with time zone");
    }
}
