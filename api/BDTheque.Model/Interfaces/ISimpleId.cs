namespace BDTheque.Model.Interfaces;

using BDTheque.Model.Attributes;

[InterfaceType]
[ApplyDefaultInterfaceMapping]
public interface ISimpleId
{
    public ushort Id { get; set; }
}
