namespace BDTheque.Model.Extensions;

using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Interfaces;

public static class ValidatorExtensions
{
    public static bool Validate<T>(this T entity) where T : Entity, IEntity
    {
        var validationResults = new List<ValidationResult>();
        entity.Validate(validationResults);
        return validationResults.Count == 0;
    }

    public static void Validate<T>(this T entity, ICollection<ValidationResult> validationResults) where T : Entity, IEntity =>
        Validator.TryValidateObject(
            entity,
            new ValidationContext(entity, null, null),
            validationResults,
            true
        );
}
