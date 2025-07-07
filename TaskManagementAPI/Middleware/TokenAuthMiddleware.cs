using TaskManagementAPI.Services;

namespace TaskManagementAPI.Middleware
{
    public class TokenAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenAuthMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            if (context.Request.Headers.TryGetValue("X-Auth-Token", out var token))
            {
                var user = await userService.GetUserByTokenAsync(token);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }

            await _next(context);
        }
    }

    public static class TokenAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenAuthMiddleware>();
        }
    }
}
