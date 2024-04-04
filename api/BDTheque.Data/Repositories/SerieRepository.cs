namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

public class SerieRepository(BDThequeContext dbContext, IServiceProvider serviceProvider)
    : EditableEntityRepository<Serie>(dbContext, serviceProvider), ISerieRepository;
