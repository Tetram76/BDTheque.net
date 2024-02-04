namespace BDTheque.Model.Interfaces;

internal interface IEntityWithId : IBaseEntity
{
    public ushort Id { get; set; }
}
