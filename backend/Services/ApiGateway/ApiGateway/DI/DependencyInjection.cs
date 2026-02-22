using System.Security.Claims;
using ApiGateway.Extensions.AuthExtensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Yarp.ReverseProxy.Swagger;
using Yarp.ReverseProxy.Swagger.Extensions;

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
        
        
        var reverseProxyConfig = configuration
            .GetSection("ReverseProxy");
        
        services.AddReverseProxy()
            .LoadFromConfig(reverseProxyConfig)
            .AddSwagger(reverseProxyConfig);
        
       
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