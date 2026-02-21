using ApiGateway.Extensions.AuthExtensions;
using ApiGateway.Handlers;
using ApiGateway.services;

namespace ApiGateway.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddControllers();
        services.AddOpenApi();
        services.AddSwaggerGen();
        services.AddHttpServices(configuration);
        services.AddAuth(configuration);
        services.AddServices();
        
        
        return services;
    }

    private static IServiceCollection AddHttpServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddHttpContextAccessor();
        services.AddTransient<UserContextHandler>();
        
        services.AddHttpClient("AuthService", client =>
        {
            client.BaseAddress = new Uri(configuration["Services:AuthService"]!);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(30);
        }).AddHttpMessageHandler<UserContextHandler>();

        return services;
    }

    private static IServiceCollection AddServices(
       this IServiceCollection services)
    {
        services.AddScoped<AuthService>();

        return services;
    }
}