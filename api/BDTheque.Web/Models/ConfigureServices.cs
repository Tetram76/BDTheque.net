namespace BDTheque.Web.Models;

public static class ConfigureServices
{
    public static IServiceCollection SetupGraphQL(this IServiceCollection services)
    {
        // services.AddScoped<VotreTypeQuery>();
        // services.AddScoped<ISchema, Schema>(serviceProvider =>
        //     new Schema(new FuncDependencyResolver(serviceProvider.GetRequiredService))
        //     {
        //         Query = serviceProvider.GetRequiredService<VotreTypeQuery>()
        //     });

        // Ajouter GraphQL
        // services.AddGraphQL(options => { options.EnableMetrics = false; }).AddSystemTextJson();

        return services;
    }
}