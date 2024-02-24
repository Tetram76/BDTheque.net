namespace BDTheque.Model.Inputs;

public abstract class AssociableCreateInput : UniqueIdCreateInput
{
    public Optional<IList<string>?> Associations { get; set; }
}

public abstract class AssociableUpdateInput : UniqueIdUpdateInput
{
    public Optional<IList<string>?> Associations { get; set; }
}

public abstract class AssociableNestedInput : UniqueIdNestedInput;
