namespace BDTheque.Web.Middlewares;

public class NotFoundClientRoutingMiddleware(RequestDelegate next)
{
    private const string Index = "/index.html";

    public async Task Invoke(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == 404)
        {
            context.Request.Path = new PathString(Index);
            await next(context);
        }
    }
}
