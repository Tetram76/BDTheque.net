namespace BDTheque.Model.Inputs;

public abstract class UniqueIdInput : VersioningInput
{
    public Optional<Guid> Id { get; set; } = Guid.Empty;
}
