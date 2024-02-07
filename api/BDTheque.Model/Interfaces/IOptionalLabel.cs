namespace BDTheque.Model.Interfaces;

using BDTheque.Model.Attributes;

[InterfaceType]
[ApplyDefaultInterfaceMapping]
public interface IOptionalLabel
{
    public char? Initiale { get; set; }
}
