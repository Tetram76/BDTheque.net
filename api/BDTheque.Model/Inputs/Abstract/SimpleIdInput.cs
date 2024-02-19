namespace BDTheque.Model.Inputs;

public abstract class SimpleIdInput : VersioningInput
{
    public Optional<ushort> Id { get; set; } = ushort.MaxValue;
}
