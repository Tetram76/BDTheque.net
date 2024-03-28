namespace BDTheque.Web.Helpers;

using System.Text;

using Newtonsoft.Json;

public static class HandleErrorHelper
{
    public static async Task HandleErrorAsync(HttpContext context, int statusCodes, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCodes;
        await context.Response.WriteAsync(
            JsonConvert.SerializeObject(
                new
                {
                    error = message
                }
            ), Encoding.UTF8
        );
    }

    public static async Task HandleErrorAsync<TException>(HttpContext context, int statusCodes, TException exception)
        where TException : Exception
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCodes;
        await context.Response.WriteAsync(
            JsonConvert.SerializeObject(
                new
                {
                    error = exception.Message,
                    exception = exception.GetType().FullName,
#if DEBUG
                    stackTrace = exception.StackTrace?.Split(Environment.NewLine)
#endif
                }
            ),
            Encoding.UTF8
        );
    }
}
