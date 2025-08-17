namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

public class CoteRepository(BDThequeContext dbContext, IServiceProvider serviceProvider)
    : EntityRepository<Cote, Guid>(dbContext, serviceProvider), ICoteRepository;
