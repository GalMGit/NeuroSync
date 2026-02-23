using System.Security.Claims;
using Shared.AuthExtensions;

namespace ApiGateway.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddAuth(configuration);
        
        return services;
    }
    
    public static IApplicationBuilder MapMiddlewares(
        this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var userId = context.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var username = context.User
                    .FindFirst(ClaimTypes.Name)?.Value;

                if (!string.IsNullOrEmpty(userId))
                    context.Request.Headers["X-User-Id"] = userId;

                if (!string.IsNullOrEmpty(username))
                    context.Request.Headers["X-Username"] = username;
            }

            await next();
        });

        return app;
    }
}