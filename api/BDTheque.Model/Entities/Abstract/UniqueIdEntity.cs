namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class UniqueIdEntity : BaseEntity, IUniqueIdEntity, IValidatableObject
{
    [ID]
    public Guid Id { get; set; }

    public static implicit operator Guid(UniqueIdEntity entity) =>
        entity.Id;

    [GraphQLIgnore]
    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        yield break;
    }
}
