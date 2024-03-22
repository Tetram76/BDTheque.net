namespace BDTheque.Data.Repositories;

using BDTheque.Model.Entities;

public interface ISerieRepository : IUniqueIdRepository<Serie>, IEditableEntityRepository<Serie>;
