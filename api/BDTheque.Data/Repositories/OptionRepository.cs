namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

public class OptionRepository(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : EntityRepository<Option, ushort>(serviceProvider, dbContext);
