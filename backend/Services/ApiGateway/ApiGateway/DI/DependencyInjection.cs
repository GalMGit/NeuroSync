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
}