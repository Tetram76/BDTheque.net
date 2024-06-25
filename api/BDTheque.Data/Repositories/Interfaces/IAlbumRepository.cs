namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Entities;

public interface IAlbumRepository : IUniqueIdRepository<Album>, IEditableEntityRepository<Album>;
