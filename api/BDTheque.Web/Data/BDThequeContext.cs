namespace BDTheque.Web.Data;

using Microsoft.EntityFrameworkCore;

public class BDThequeContext : DbContext
{
    public BDThequeContext(DbContextOptions<BDThequeContext> options) : base(options)
    {
    }

    // Définissez vos DbSets ici
    // public DbSet<YourModel> YourModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurez vos modèles et relations ici
    }
}