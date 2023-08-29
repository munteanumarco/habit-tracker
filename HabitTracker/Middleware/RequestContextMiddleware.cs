namespace HabitTracker.Middleware;

public class RequestContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ExtractUserId(context);
        await _next(context);
    }

    private static void ExtractUserId(HttpContext context)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            context.Items["userId"] = userId;
        }
    }
}