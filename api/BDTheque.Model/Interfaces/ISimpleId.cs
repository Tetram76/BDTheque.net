namespace BDTheque.Model.Interfaces;

[InterfaceType]
public interface ISimpleId
{
    [ID]
    public ushort Id { get; set; }
}
