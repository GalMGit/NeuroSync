using Shared.Contracts.Options;

namespace AuthService.API.Extensions;

public static class AuthExtension
{
    
    public static void AddJwtConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

        services.AddOptions<JwtOptions>()
            .Validate(o => !string.IsNullOrEmpty(o.SecretKey),
                "SecretKey is required")
            .ValidateOnStart();
    }
}