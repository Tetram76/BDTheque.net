namespace BDTheque.Model.Inputs;

public abstract class UniqueIdCreateInput : BaseCreateInput;

public abstract class UniqueIdUpdateInput : BaseUpdateInput
{
    [ID]
    [Required]
    public Guid Id { get; set; } = Guid.Empty;
}

public abstract class UniqueIdNestedInput : BaseNestedInput
{
    [ID]
    [Required]
    public Guid Id { get; set; } = Guid.Empty;
}
