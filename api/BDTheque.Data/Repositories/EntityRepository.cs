namespace BDTheque.Data.Repositories;

using BDTheque.Data.Attributes;
using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Interfaces;

using FluentValidation;

using Humanizer;

using Microsoft.EntityFrameworkCore.ChangeTracking;

[EntityRepository]
public abstract class EntityRepository<TEntity, TKey>(BDThequeContext dbContext, IServiceProvider serviceProvider) : IEntityRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly Lazy<IValidator<TEntity>?> _entityValidator = new(() => serviceProvider.GetService(typeof(IValidator<TEntity>)) as IValidator<TEntity>, true);

    protected IValidator<TEntity>? EntityValidator => _entityValidator.Value;

    protected IServiceProvider ServiceProvider { get; } = serviceProvider;
    protected BDThequeContext DbContext { get; } = dbContext;

    public EntityEntry<TEntity> GetEntry(TEntity entity) =>
        DbContext.Set<TEntity>().Entry(entity);

    public async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default) =>
        await GetByIdOrDefault(id, cancellationToken)
        ?? throw new InvalidOperationException($"{typeof(TEntity).Name.Humanize(LetterCasing.Sentence)} id \"{id}\" not found");

    public async Task<TEntity?> GetByIdOrDefault(TKey id, CancellationToken cancellationToken = default) =>
        await DbContext.Set<TEntity>().FindAsync([id], cancellationToken);
}
