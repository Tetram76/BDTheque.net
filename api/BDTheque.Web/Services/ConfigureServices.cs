namespace BDTheque.Web.Services;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Listeners;
using BDTheque.GraphQL.Mutations;
using BDTheque.GraphQL.Queries;
using BDTheque.GraphQL.Subscriptions;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using StackExchange.Redis;

public static class ConfigureServices
{
    private static string? GetConnectionString(this IConfiguration configurationManager) => configurationManager.GetConnectionString("BDThequeDatabase");

    private static ConfigurationOptions RedisConfiguration(this IConfiguration configurationManager) =>
        new()
        {
            EndPoints =
            {
                "localhost:6379"
            }
        };

    public static IServiceCollection SetupDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContextPool<BDThequeContext>(
            options =>
                options.UseNpgsql(
                    configuration.GetConnectionString(),
                    builder => builder.MigrationsAssembly("BDTheque.Data")
                )
        );

        return services;
    }

    public static IRequestExecutorBuilder SetupGraphQLSchema(this IServiceCollection services) =>
        services
            .AddGraphQLServer()
            .RegisterDbContext<BDThequeContext>()
            .AddBDThequeGraphQL()
            .AddMutationConventions()
            .AddProjections() // Pour les requêtes de sous-sélection
            .AddFiltering() // Pour les filtres
            .AddSorting(); // Pour le tri

    public static IRequestExecutorBuilder SetupGraphQLPipeline(this IRequestExecutorBuilder builder, ConfigurationManager configuration, IWebHostEnvironment environment) =>
        builder
            .AddHttpRequestInterceptor<CustomRequestInterceptor>()
            .ModifyRequestOptions(options => options.IncludeExceptionDetails = environment.IsDevelopment())
            .RegisterDbContext<BDThequeContext>()
            .AddDefaultTransactionScopeHandler()
            .AddRedisSubscriptions(_ => ConnectionMultiplexer.Connect(configuration.RedisConfiguration()))
            .AddMaxExecutionDepthRule(10, true, true)
            .AddDiagnosticEventListener<ServerEventListener>()
            .AddDiagnosticEventListener<ExecutionEventListener>()
            .AddDiagnosticEventListener<DataLoaderEventListener>();
}
