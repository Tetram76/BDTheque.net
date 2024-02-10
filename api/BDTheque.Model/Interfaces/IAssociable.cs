namespace BDTheque.Model.Interfaces;

[InterfaceType]
public interface IAssociable
{
    public IList<string>? Associations { get; set; }
}
