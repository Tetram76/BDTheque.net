// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Linq.Expressions;

public static class EntityTypeBuilderExtension
{
    internal static string GetColumnName<TEntity, TProperty>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TProperty>> propertyExpression)
        where TEntity : class
        => entity.Property(propertyExpression).Metadata.Name;
}
