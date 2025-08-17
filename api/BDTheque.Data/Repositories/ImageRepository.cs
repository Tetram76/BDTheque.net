namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

public class ImageRepository(BDThequeContext dbContext, IServiceProvider serviceProvider)
    : EntityRepository<Image, Guid>(dbContext, serviceProvider), IImageRepository;
