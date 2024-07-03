using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OriginWhitelistMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HashSet<string> _whitelistedOrigins;

    public OriginWhitelistMiddleware(RequestDelegate next, IEnumerable<string> whitelistedOrigins)
    {
        _next = next;
        _whitelistedOrigins = new HashSet<string>(whitelistedOrigins);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var origin = context.Request.Headers["Origin"].FirstOrDefault();

        if (origin != null && !_whitelistedOrigins.Contains(origin))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Not authorized.");
            return;
        }

        await _next(context);
    }
}

public static class OriginWhitelistMiddlewareExtensions
{
    public static IApplicationBuilder UseOriginWhitelist(
        this IApplicationBuilder builder, IEnumerable<string> whitelistedOrigins)
    {
        return builder.UseMiddleware<OriginWhitelistMiddleware>(whitelistedOrigins);
    }
}
