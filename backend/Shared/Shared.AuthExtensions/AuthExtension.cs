using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Options;

namespace Shared.AuthExtensions;

public static class AuthExtension
{
    public static void AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

        services.AddOptions<JwtOptions>()
            .Validate(o =>
                    !string.IsNullOrEmpty(o.SecretKey),
                "SecretKey is required")
            .ValidateOnStart();

        var jwtOptions = configuration
            .GetSection("JwtOptions")
            .Get<JwtOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                    ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers.Authorization.FirstOrDefault();

                        if (!string.IsNullOrEmpty(authHeader)
                            && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = authHeader["Bearer ".Length..].Trim();
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();
    }
}