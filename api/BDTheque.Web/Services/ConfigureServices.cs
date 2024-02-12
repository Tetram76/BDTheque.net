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
using QueryableStringContainsHandler = BDTheque.GraphQL.Handlers.QueryableStringContainsHandler;
using QueryableStringNotContainsHandler = BDTheque.GraphQL.Handlers.QueryableStringNotContainsHandler;

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

    public static IServiceCollection SetupDb(this IServiceCollection services, bool IsDevEnvironment, IConfiguration configuration, Action<DbContextOptionsBuilder>? optionsAction = null)
        => services.SetupDb(IsDevEnvironment, configuration.GetConnectionString(), optionsAction);

    public static IServiceCollection SetupDb(this IServiceCollection services, bool isDevEnvironment, string? connectionString, Action<DbContextOptionsBuilder>? optionsAction = null)
    {
        services.AddDbContextPool<BDThequeContext>(
            options =>
            {
                options.ConfigureWarnings(
                    builder =>
                        builder.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning)
                );

                if (isDevEnvironment)
                    options
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();

                options.UseNpgsql(
                    connectionString,
                    builder => builder.MigrationsAssembly("BDTheque.Data")
                );
                optionsAction?.Invoke(options);
            }
        );

        return services;
    }

    public static IRequestExecutorBuilder SetupGraphQLSchema(this IServiceCollection services, bool isDevEnvironment)
        => services
            .AddGraphQLServer()
            .AllowIntrospection(isDevEnvironment)
            .ModifyOptions(
                options =>
                {
                    options.EnableTrueNullability = true;
                    options.UseXmlDocumentation = false;
                    options.ValidatePipelineOrder = true;
                    options.StrictRuntimeTypeValidation = true;
                }
            )
            .RegisterDbContext<BDThequeContext>()
            .AddType(new UuidType('D'))
            .AddBDThequeGraphQLTypes()
            .AddBDThequeGraphQLExtensions()
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
            .AddSorting();

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
