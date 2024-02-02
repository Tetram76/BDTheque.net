namespace BDTheque.Data.Entities;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public static partial class ModelBuilderExtensions
{
    private static string GetColumnName<TEntity, TProperty>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TProperty>> propertyExpression) where TEntity : BaseEntity =>
        entity.Property(propertyExpression).Metadata.Name;

    private static void SetupVersioning<T>(EntityTypeBuilder<T> entity) where T : BaseEntity
    {
        entity.Property(e => e.CreatedAt).HasColumnType("timestamp(3) with time zone");
        entity.Property(e => e.UpdatedAt).HasColumnType("timestamp(3) with time zone");
    }
}
