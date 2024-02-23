namespace BDTheque.GraphQL.Listeners;

using Microsoft.Extensions.Logging;

public sealed class RequestScope(DateTime start, ILogger logger) : IDisposable
{
    // this is invoked at the end of the `ExecuteRequest` operation
    public void Dispose()
    {
        DateTime end = DateTime.UtcNow;
        TimeSpan elapsed = end - start;

        logger.LogInformation("Request finished after {Ticks} ticks", elapsed.Ticks);
    }
}
