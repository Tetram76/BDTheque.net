namespace BDTheque.Data.Repositories;

using System.ComponentModel.DataAnnotations;
using BDTheque.Data.Context;
using BDTheque.Data.Validators;
using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Interfaces;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ValidationException = BDTheque.Data.Exceptions.ValidationException;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

public abstract class EntityRepository<TEntity, TKey>(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : IEntityRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public IServiceProvider ServiceProvider { get; } = serviceProvider;
    public BDThequeContext DbContext { get; } = dbContext;

    public EntityEntry<TEntity> GetEntry(TEntity entity) =>
        DbContext.Set<TEntity>().Entry(entity);

    public async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default) =>
        await DbContext.Set<TEntity>().SingleAsync(entity => entity.Id.Equals(id), cancellationToken);

    public async Task<TEntity?> GetByIdOrDefault(TKey id, CancellationToken cancellationToken = default) =>
        await DbContext.Set<TEntity>().SingleOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);
}
