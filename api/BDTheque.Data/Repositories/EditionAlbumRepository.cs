namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

public class EditionRepository(BDThequeContext dbContext, IServiceProvider serviceProvider)
    : EntityRepository<Edition, Guid>(dbContext, serviceProvider), IEditionRepository;
