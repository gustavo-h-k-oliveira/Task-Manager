using System.Diagnostics;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");

        await _next(context);

        watch.Stop();
        Console.WriteLine($"[Response] Status: {context.Response.StatusCode} - Time: {watch.ElapsedMilliseconds} ms");
    }
}