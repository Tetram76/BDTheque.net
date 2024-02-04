namespace BDTheque.Model.Interfaces;

public interface IEntityWithId : IBaseEntity
{
    public ushort Id { get; set; }
}
