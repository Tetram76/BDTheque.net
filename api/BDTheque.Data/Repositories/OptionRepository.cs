namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

public class OptionRepository(BDThequeContext dbContext, IServiceProvider serviceProvider)
    : EntityRepository<Option, ushort>(dbContext, serviceProvider);
