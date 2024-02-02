namespace BDTheque.Web.Middlewares;

using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using Serilog.Events;

public sealed class RequestLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var watch = new Stopwatch();
        watch.Start();
        try
        {
            await next(httpContext);
        }
        finally
        {
            watch.Stop();
            Log.Write(
                httpContext.Response.StatusCode >= 400 ? LogEventLevel.Error : LogEventLevel.Information,
                "{RequestMethod} {ResponseStatusCode} {WatchElapsedMilliseconds} {DisplayUrl}",
                httpContext.Request.Method, httpContext.Response.StatusCode, watch.ElapsedMilliseconds + "ms",
                httpContext.Request.GetDisplayUrl()
            );
        }
    }
}
