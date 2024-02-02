namespace BDTheque.GraphQL.Listeners;

using HotChocolate.AspNetCore.Instrumentation;
using Microsoft.AspNetCore.Http;
using Serilog;

public class ServerEventListener : ServerDiagnosticEventListener
{
    public override void HttpRequestError(HttpContext context, Exception exception)
    {
        Log.Error(exception, "HttpRequestError");
        base.HttpRequestError(context, exception);
    }
}
