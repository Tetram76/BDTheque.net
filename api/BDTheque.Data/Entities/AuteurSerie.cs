﻿namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<AuteurSerie> entity)
    {
        entity.ToTable("AuteursSeries");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.AuteurId,
                e.SerieId
            }
        ).IsUnique();

        entity.HasOne(d => d.Auteur).WithMany(p => p.AuteursSeries).HasForeignKey(d => d.AuteurId);
        entity.HasOne(d => d.Serie).WithMany(p => p.AuteursSeries).HasForeignKey(d => d.SerieId);
    }
}
