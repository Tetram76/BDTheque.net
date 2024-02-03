namespace BDTheque.Tests.Helpers;

using System.Runtime.CompilerServices;
using BDTheque.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using Schema = HotChocolate.Schema;

public static class TestServices
{
    static TestServices()
    {
        Services = new ServiceCollection()
            .SetupGraphQLSchema()
            .ModifyOptions(options => { options.SortFieldsByName = true; })
            .ModifyRequestOptions(options => options.IncludeExceptionDetails = true)
            .Services
            .AddSingleton(
                sp => new RequestExecutorProxy(sp.GetRequiredService<IRequestExecutorResolver>(), Schema.DefaultName)
            )
            .BuildServiceProvider();

        Executor = Services.GetRequiredService<RequestExecutorProxy>();
    }

    public static IServiceProvider Services { get; }

    public static RequestExecutorProxy Executor { get; }

    public static async Task<string> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
    {
        await using AsyncServiceScope scope = Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        IQueryRequest request = requestBuilder.Create();

        await using IExecutionResult result = await Executor.ExecuteAsync(request, cancellationToken);

        result.ExpectQueryResult();

        return result.ToJson();
    }

    public static async IAsyncEnumerable<string> ExecuteRequestAsStreamAsync(
        Action<IQueryRequestBuilder> configureRequest,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using AsyncServiceScope scope = Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        IQueryRequest request = requestBuilder.Create();

        await using IExecutionResult result = await Executor.ExecuteAsync(request, cancellationToken);

        await foreach (IQueryResult? element in result.ExpectResponseStream().ReadResultsAsync().WithCancellation(cancellationToken))
            await using (element)
            {
                yield return element.ToJson();
            }
    }
}
