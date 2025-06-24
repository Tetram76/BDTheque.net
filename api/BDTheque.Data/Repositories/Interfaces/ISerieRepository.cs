namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Entities;

public interface ISerieRepository : IUniqueIdRepository<Serie>, IEditableEntityRepository<Serie>;
