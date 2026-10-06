public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthenticationMiddleware> _logger;

    public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check for the presence of an Authorization header
        if (!context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Authorization header is missing.");
            return;
        }

        // Validate the token (this is a placeholder; implement your own validation logic)
        if (!IsValidToken(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token.");
            return;
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }

    private bool IsValidToken(string token)
    {
        // Implement your token validation logic here
        // For demonstration purposes, we'll just check if the token is "valid-token"
        return token == "valid-token";
    }
}