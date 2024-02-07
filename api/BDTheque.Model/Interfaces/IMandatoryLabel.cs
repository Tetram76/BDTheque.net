namespace BDTheque.Model.Interfaces;

using BDTheque.Model.Attributes;

[InterfaceType]
[ApplyDefaultInterfaceMapping]
public interface IMandatoryLabel
{
    public char Initiale { get; set; }
}
