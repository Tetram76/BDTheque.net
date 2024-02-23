namespace BDTheque.Model.Inputs;

public abstract class AssociableInput : UniqueIdInput
{
    public Optional<IList<string>> Associations { get; set; }
}
