namespace BDTheque.Model.Interfaces;

using BDTheque.Model.Attributes;

[InterfaceType]
[ApplyDefaultInterfaceMapping]
public interface IAssociable
{
    public IList<string>? Associations { get; set; }
}
