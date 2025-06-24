namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

public class AlbumRepository(BDThequeContext dbContext, IServiceProvider serviceProvider)
    : EditableEntityRepository<Album>(dbContext, serviceProvider), IAlbumRepository;
