namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class UniqueIdEntity : BaseEntity, IUniqueIdEntity, IValidatableObject
{
    [ID]
    public Guid Id { get; set; }

    [GraphQLIgnore]
    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        yield break;
    }
}
