namespace BDTheque.Model.Interfaces;

[InterfaceType("IAssociable")]
public interface IAssociableEntity
{
    public IList<string>? Associations { get; set; }
}
