namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

public class AlbumRepository(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : EditableEntityRepository<Album>(serviceProvider, dbContext), IAlbumRepository;
