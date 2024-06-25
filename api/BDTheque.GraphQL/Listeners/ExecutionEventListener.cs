namespace BDTheque.GraphQL.Listeners;

using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;

using Microsoft.Extensions.Logging;

using Serilog;

public class ExecutionEventListener(ILogger<ExecutionEventListener> logger) : ExecutionDiagnosticEventListener
{
    public override void RequestError(IRequestContext context, Exception exception) =>
        Log.Error(exception, "A request error occurred!");

    public override IDisposable ExecuteRequest(IRequestContext context)
    {
        DateTime start = DateTime.UtcNow;

        return new RequestScope(start, logger);
    }
}
