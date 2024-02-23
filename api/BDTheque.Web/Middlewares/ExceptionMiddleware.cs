namespace BDTheque.Web.Middlewares;

using System.Net;
using BDTheque.Web.Helpers;
using Serilog;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        async Task HandleErrorAsync(Exception exception, HttpStatusCode httpStatusCode, string message)
        {
            Log.Error(exception, message);
            await HandleErrorHelper.HandleErrorAsync(httpContext, (int) httpStatusCode, exception);
        }

        try
        {
            await next(httpContext);
        }
        catch (HostAbortedException)
        {
            // no log, no response required
        }
        catch (SchemaException schemaException)
        {
            await HandleErrorAsync(schemaException, HttpStatusCode.InternalServerError, "Error with GraqhQL schema");
        }
        catch (ArgumentNullException nullException)
        {
            await HandleErrorAsync(nullException, HttpStatusCode.NotFound, "Argument is null");
        }
        catch (ArgumentException argumentException)
        {
            await HandleErrorAsync(argumentException, HttpStatusCode.UnprocessableEntity, "Argument is wrong");
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(exception, HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }
}
