namespace BDTheque.Web.Services;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Filters;
using BDTheque.GraphQL.Listeners;
using HotChocolate.Data.Filters;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

public static class ConfigureServices
{
    private static string? GetConnectionString(this IConfiguration configurationManager)
        => configurationManager.GetConnectionString("BDThequeDatabase");

    private static ConfigurationOptions RedisConfiguration(this IConfiguration _)
        => new()
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

    public static IRequestExecutorBuilder SetupGraphQLSchema(this IServiceCollection services)
        => services
            .AddGraphQLServer()
            .ModifyOptions(options => options.EnableTrueNullability = true)
            .BindRuntimeType<char, StringType>()
            .BindRuntimeType<ushort, UnsignedShortType>()
            .AddMutationConventions()
            .AddProjections() // Pour les requêtes de sous-sélection
            .AddFiltering( // Pour les filtres
                descriptor =>
                    descriptor
                        .AddDefaults()
                        .BindRuntimeType<char, CharOperationFilterInputType>()
                        .BindRuntimeType<char?, CharOperationFilterInputType>()
                        .BindRuntimeType<ushort, ComparableOperationFilterInputType<NonNullType<UnsignedIntType>>>()
                        .BindRuntimeType<ushort?, ComparableOperationFilterInputType<UnsignedIntType>>()
            )
            .AddSorting() // Pour le tri
            .RegisterDbContext<BDThequeContext>()
            .AddBDThequeGraphQLTypes()
            .AddBDThequeGraphQLExtensions();

    public static IRequestExecutorBuilder SetupGraphQLPipeline(this IRequestExecutorBuilder builder, ConfigurationManager configuration, IWebHostEnvironment environment)
        => builder
            .AddHttpRequestInterceptor<CustomRequestInterceptor>()
            .ModifyRequestOptions(options => options.IncludeExceptionDetails = environment.IsDevelopment())
            .AddDefaultTransactionScopeHandler()
            .AddRedisSubscriptions(_ => ConnectionMultiplexer.Connect(configuration.RedisConfiguration()))
            .AddMaxExecutionDepthRule(10, true, true)
            .AddTypeConverter<DateTimeOffset, DateTime>(t => t.UtcDateTime)
            .AddTypeConverter<DateTime, DateTimeOffset>(t => t.Kind is DateTimeKind.Unspecified ? DateTime.SpecifyKind(t, DateTimeKind.Utc) : t)
            .AddDiagnosticEventListener<ServerEventListener>()
            .AddDiagnosticEventListener<ExecutionEventListener>()
            .AddDiagnosticEventListener<DataLoaderEventListener>();
}
