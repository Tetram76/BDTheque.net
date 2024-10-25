namespace BDTheque.Data.Repositories;

using System.ComponentModel.DataAnnotations;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Data.Validators;
using BDTheque.Model.Interfaces;

using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using ValidationException = BDTheque.Data.Exceptions.ValidationException;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

public abstract class EditableEntityRepository<TEntity>(
    BDThequeContext dbContext,
    IServiceProvider serviceProvider
) : EntityRepository<TEntity, Guid>(dbContext, serviceProvider), IEditableEntityRepository<TEntity>
    where TEntity : class, IUniqueIdEntity
{
    public async Task<EntityEntry<TEntity>> Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Validate(entity, cancellationToken);
        EntityEntry<TEntity> entry = DbContext.Set<TEntity>().Add(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task<EntityEntry<TEntity>> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        await ValidateOrReload(entity, cancellationToken);
        EntityEntry<TEntity> entry = DbContext.Set<TEntity>().Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task<EntityEntry<TEntity>> Remove(TEntity entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<TEntity> entry = DbContext.Set<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task Reload(TEntity entity, CancellationToken cancellationToken = default) =>
        await GetEntry(entity).ReloadAsync(cancellationToken);

    public async Task Validate(TEntity entity, CancellationToken cancellationToken = default)
    {
        ValidationResult? validationError = await GetValidationResult(entity, cancellationToken);
        if (validationError is not null)
            throw new ValidationException(validationError.ErrorMessage);
    }

    public async Task ValidateOrReload(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await Validate(entity, cancellationToken);
        }
        catch (ValidationException)
        {
            await Reload(entity, cancellationToken);
            throw;
        }
    }

    private async Task<ValidationResult?> GetValidationResult(TEntity entity, CancellationToken cancellationToken)
    {
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(entity, new ValidationContext(entity, ServiceProvider, null), validationResults, false))
            return validationResults[0];

        if (EntityValidator is null)
            return ValidationResult.Success;

        FluentValidation.Results.ValidationResult validationResult = await EntityValidator.ValidateAsync(
            new ValidationContext<TEntity>(
                entity,
                null,
                new RulesetValidatorSelector(
                    [
                        EntityValidator<TEntity>.DefaultRuleSet,
                        GetEntry(entity).State == EntityState.Detached ? EntityValidator<TEntity>.AddRuleSet : EntityValidator<TEntity>.UpdateRuleSet
                    ]
                )
            ),
            cancellationToken
        );
        if (validationResult.IsValid)
            return ValidationResult.Success;

        ValidationFailure failure = validationResult.Errors[0];
        return new ValidationResult(failure.ErrorMessage, [failure.PropertyName]);
    }
}
