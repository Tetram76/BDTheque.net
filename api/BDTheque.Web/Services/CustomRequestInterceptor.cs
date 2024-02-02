namespace BDTheque.Web.Services;

using HotChocolate.AspNetCore;
using HotChocolate.Execution;

public class CustomRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        requestBuilder.SkipExecutionDepthAnalysis();

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}
