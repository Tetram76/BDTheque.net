namespace BDTheque.Web.Services;

using BDTheque.Data.Context;
using BDTheque.GraphQL.Listeners;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StackExchange.Redis;
using Path = System.IO.Path;
using QueryableStringContainsHandler = BDTheque.GraphQL.Handlers.QueryableStringContainsHandler;
using QueryableStringNotContainsHandler = BDTheque.GraphQL.Handlers.QueryableStringNotContainsHandler;

public static class ConfigureServices
{
    public sealed class Options(IConfiguration configuration)
    {
        public string? DatabaseConnectionString => configuration.GetConnectionString("BDThequeDatabase");

        public string RedisEndpoint => configuration.GetConnectionString("RedisEndpoint") ?? "localhost:6379";

        public bool SetupPipeline { get; init; } = true;
        public Action<IRequestExecutorBuilder>? ModifyPipeline { get; init; } = null;
        public bool Debug { get; init; } = true;
    }

    public static IServiceCollection SetupApp(this IServiceCollection services, Options options)
    {
        services
            .SetupDb(options);

        IRequestExecutorBuilder requestExecutorBuilder = services
            .SetupGraphQLSchema(options);

        if (options.SetupPipeline)
            requestExecutorBuilder.SetupGraphQLPipeline(options);

        options.ModifyPipeline?.Invoke(requestExecutorBuilder);

        return services;
    }

    public static IConfigurationBuilder AddContextualJsonFile(this IConfigurationBuilder builder, string path, bool optional = false, bool reloadOnChange = false)
    {
        builder.AddJsonFile(path, optional, reloadOnChange);
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (!string.IsNullOrEmpty(environment))
            builder.AddJsonFile(Path.ChangeExtension(path, $".{environment}{Path.GetExtension(path)}"), true, reloadOnChange);
        return builder;
    }

    private static IServiceCollection SetupDb(this IServiceCollection services, Options appOptions)
        => services.AddDbContextPool<BDThequeContext>(
            options =>
            {
                options
                    .ConfigureWarnings(
                        builder =>
                        {
                            builder.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning);
                        }
                    )
                    .EnableSensitiveDataLogging(appOptions.Debug)
                    .EnableDetailedErrors(appOptions.Debug)
                    .UseNpgsql(
                        appOptions.DatabaseConnectionString,
                        builder => builder.MigrationsAssembly("BDTheque.Data")
                    );
            }
        );

    private static IRequestExecutorBuilder SetupGraphQLSchema(this IServiceCollection services, Options appOptions)
        => services
            .AddGraphQLServer()
            .AllowIntrospection(appOptions.Debug)
            .ModifyRequestOptions(o => o.IncludeExceptionDetails = appOptions.Debug)
            .ModifyOptions(
                options =>
                {
                    options.EnableTrueNullability = true;
                    options.UseXmlDocumentation = false;
                    options.ValidatePipelineOrder = true;
                    options.StrictRuntimeTypeValidation = true;
                    options.SortFieldsByName = appOptions.Debug;
                }
            )
            // .BindRuntimeType<char, StringType>()
            .BindRuntimeType<ushort, UnsignedShortType>()
            .AddMutationConventions()
            .SetPagingOptions(
                new PagingOptions
                {
                    IncludeTotalCount = true
                }
            )
            .AddProjections() // Pour les requêtes de sous-sélection
            .AddFiltering(
                descriptor =>
                    descriptor
                        .AddDefaults()
                        // .BindRuntimeType<char?, CharOperationFilterInputType>()
                        // .BindRuntimeType<char?, CharOperationFilterInputType>()
                        .BindRuntimeType<char, StringOperationFilterInputType>()
                        .BindRuntimeType<char?, StringOperationFilterInputType>()
                        .BindRuntimeType<ushort, ComparableOperationFilterInputType<NonNullType<UnsignedIntType>>>()
                        .BindRuntimeType<ushort?, ComparableOperationFilterInputType<UnsignedIntType>>()
                        .Provider(
                            new QueryableFilterProvider(
                                providerDescriptor => providerDescriptor
                                    .AddFieldHandler<QueryableStringContainsHandler>()
                                    .AddFieldHandler<QueryableStringNotContainsHandler>()
                                    .AddDefaultFieldHandlers()
                            )
                        )
            )
            .AddSorting() // Pour le tri
            .RegisterDbContext<BDThequeContext>()
            .AddBDThequeGraphQLTypes()
            .AddBDThequeGraphQLExtensions()
            .AddRedisSubscriptions(
                _ => ConnectionMultiplexer.Connect(
                    new ConfigurationOptions
                    {
                        EndPoints =
                        {
                            appOptions.RedisEndpoint ?? "localhost:6379"
                        }
                    }
                )
            )
            .AddTypeConverter<DateTimeOffset, DateTime>(t => t.UtcDateTime)
            .AddTypeConverter<DateTime, DateTimeOffset>(t => t.Kind is DateTimeKind.Unspecified ? DateTime.SpecifyKind(t, DateTimeKind.Utc) : t);

    private static IRequestExecutorBuilder SetupGraphQLPipeline(this IRequestExecutorBuilder builder, Options appOptions)
        => builder
            .AddHttpRequestInterceptor<CustomRequestInterceptor>()
            .AddDefaultTransactionScopeHandler()
            .AddMaxExecutionDepthRule(10, true, true)
            .AddDiagnosticEventListener<ServerEventListener>()
            .AddDiagnosticEventListener<ExecutionEventListener>()
            .AddDiagnosticEventListener<DataLoaderEventListener>();
}
