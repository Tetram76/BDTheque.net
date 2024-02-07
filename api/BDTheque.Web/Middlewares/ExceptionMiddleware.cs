namespace BDTheque.Web.Middlewares;

using System.Net;
using BDTheque.Web.Helpers;
using Serilog;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (SchemaException schemaException)
        {
            await HandleError(schemaException, HttpStatusCode.InternalServerError, "Error with GraqhQL schema");
        }
        catch (ArgumentNullException nullException)
        {
            await HandleError(nullException, HttpStatusCode.NotFound, "Argument is null");
        }
        catch (ArgumentException argumentException)
        {
            await HandleError(argumentException, HttpStatusCode.UnprocessableEntity, "Argument is wrong");
        }
        catch (Exception exception)
        {
            await HandleError(exception, HttpStatusCode.InternalServerError, "Something went wrong");
        }

        return;

        async Task HandleError(Exception exception, HttpStatusCode httpStatusCode, string message)
        {
            Log.Error(exception, message);
            await HandleErrorHelper.HandleErrorAsync(httpContext, (int) httpStatusCode, exception);
        }
    }
}
