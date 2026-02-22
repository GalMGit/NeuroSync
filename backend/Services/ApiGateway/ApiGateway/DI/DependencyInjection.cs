using ApiGateway.Extensions.AuthExtensions;

namespace ApiGateway.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddOpenApi();
        services.AddSwaggerGen();
        services.AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));
        services.AddAuth(configuration);
        
        
        return services;
    }
    
}