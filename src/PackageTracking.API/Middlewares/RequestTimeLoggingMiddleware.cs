
using PackageTracking.Domain.Excpetions;
using System.Diagnostics;
using System.Net;

namespace PackageTracking.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopWatch.Stop();

        if (stopWatch.ElapsedMilliseconds > 4000)
            logger.LogWarning("Request: {Method} {Path} took {ElapsedMilliseconds} ms", context.Request.Method, context.Request.Path, stopWatch.ElapsedMilliseconds);
    }
}
