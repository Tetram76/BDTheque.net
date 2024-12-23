namespace BDTheque.Model.Inputs;

public class StaticNestedInput : BaseNestedInput
{
    [ID]
    [Required]
    public ushort Id { get; set; }
}
