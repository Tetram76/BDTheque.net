namespace BDTheque.Data.Repositories;

using BDTheque.Model.Entities;

public interface IAlbumRepository : IUniqueIdRepository<Album>, IEditableEntityRepository<Album>;
