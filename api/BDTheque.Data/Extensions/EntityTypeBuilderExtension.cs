namespace BDTheque.Data.Extensions;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class EntityTypeBuilderExtension
{
    internal static string GetColumnName<TEntity, TProperty>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TProperty>> propertyExpression)
        where TEntity : class
        => entity.Property(propertyExpression).Metadata.Name;
}
