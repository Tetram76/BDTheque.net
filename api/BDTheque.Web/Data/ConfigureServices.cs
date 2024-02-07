namespace BDTheque.Web.Data;

using Microsoft.EntityFrameworkCore;

public static class ConfigureServices
{
    public static IServiceCollection SetupDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<BDThequeContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("BDThequeDatabase"))
        );

        return services;
    }
}