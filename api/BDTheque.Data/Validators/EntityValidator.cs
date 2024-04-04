namespace BDTheque.Data.Validators;

using System.Linq.Expressions;

using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Interfaces;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public abstract class EntityValidator<TEntity> : AbstractValidator<TEntity>
    where TEntity : class, IUniqueIdEntity
{
    public const string DefaultRuleSet = "Default";
    public const string AddRuleSet = "OnAdd";
    public const string UpdateRuleSet = "OnUpdate";

    protected abstract IEditableEntityRepository<TEntity> EntityRepository { get; }

    public bool MustValidate<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> expression)
    {
        EntityEntry<TEntity> entry = EntityRepository.GetEntry(entity);
        return entry.State is not (EntityState.Deleted or EntityState.Unchanged)
               && (entry.State == EntityState.Detached || entry.Property(expression).IsModified);
    }

    protected EntityValidator()
    {
        RuleSet(DefaultRuleSet, GetDefaultRuleSet);
        RuleSet(AddRuleSet, GetAddRuleSet);
        RuleSet(UpdateRuleSet, GetUpdateRuleSet);
    }

    protected virtual void GetDefaultRuleSet()
    {
    }

    protected virtual void GetAddRuleSet()
    {
    }

    protected virtual void GetUpdateRuleSet()
    {
    }
}
