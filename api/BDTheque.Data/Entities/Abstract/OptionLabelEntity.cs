namespace BDTheque.Data.Entities;

using System.Linq.Expressions;

using BDTheque.Model.Entities.Abstract;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    private static void SetupOptionalInitiale<T, TProperty>(this EntityTypeBuilder<T> entity, Expression<Func<T, TProperty>> propertyExpression) where T : OptionalLabelEntity
    {
        entity.Property(e => e.Initiale)
            .HasMaxLength(1)
            .HasColumnType("character(1)")
            .HasComputedColumnSql($"(upper({entity.GetColumnName(propertyExpression).ToSnakeCase()}))::character(1)", true);
    }
}
