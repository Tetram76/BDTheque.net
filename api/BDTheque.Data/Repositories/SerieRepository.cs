namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

public class SerieRepository(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : EditableEntityRepository<Serie>(serviceProvider, dbContext), ISerieRepository;
