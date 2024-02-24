namespace BDTheque.Model.Inputs;

public abstract class UniqueIdCreateInput : VersioningCreateInput;

public abstract class UniqueIdUpdateInput : VersioningUpdateInput
{
    [ID]
    [Required]
    public Guid Id { get; set; } = Guid.Empty;
}

public abstract class UniqueIdNestedInput : VersioningNestedInput
{
    [ID]
    [Required]
    public Guid Id { get; set; } = Guid.Empty;
}
