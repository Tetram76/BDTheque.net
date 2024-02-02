namespace BDTheque.Web.Services;

using BDTheque.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection SetupDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<BDThequeContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("BDThequeDatabase"),
                builder => builder.MigrationsAssembly("BDTheque.Data")
            )
        );

        return services;
    }

    public static IServiceCollection SetupGraphQL(this IServiceCollection services) =>
        // services.AddScoped<VotreTypeQuery>();
        // services.AddScoped<ISchema, Schema>(serviceProvider =>
        //     new Schema(new FuncDependencyResolver(serviceProvider.GetRequiredService))
        //     {
        //         Query = serviceProvider.GetRequiredService<VotreTypeQuery>()
        //     });
        // Ajouter GraphQL
        // services.AddGraphQL(options => { options.EnableMetrics = false; }).AddSystemTextJson();
        services;
}
