
using PackageTracking.Domain.Excpetions;
using System.Net;

namespace PackageTracking.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch(NotFoundException notFound)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("An error occurred. Please try again later.");

            logger.LogWarning(notFound.Message);
        }
        catch (ForbiddenAccessException forbidden)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.Response.WriteAsync("You are not authorized to perform this action.");
            logger.LogWarning(forbidden.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("An error occurred. Please try again later.");
        }
    }
}
