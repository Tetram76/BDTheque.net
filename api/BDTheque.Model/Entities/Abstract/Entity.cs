namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

[ApplyObjectMapping]
[ApplyFilterInputMapping]
public abstract class Entity : IEntity, IValidatableObject
{
    [GraphQLIgnore]
    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        yield break;
    }
}
