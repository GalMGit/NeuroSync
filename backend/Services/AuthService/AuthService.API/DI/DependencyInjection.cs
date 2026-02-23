using AuthService.API.Extensions;
using AuthService.BLL.Mappers;
using AuthService.BLL.Services;
using AuthService.CORE.Interfaces.IRepositories;
using AuthService.CORE.Interfaces.IServices;
using AuthService.DAL.Database.DatabaseContext;
using AuthService.DAL.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NeuroSync.MinimalApi.Endpoints;

namespace AuthService.API.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddSwaggerGen();
        services.AddJwtConfiguration(configuration);
        services.AddEndpoints(typeof(Program).Assembly);
        services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
        services.AddServices(configuration);
        
        return services;
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(x =>
        {
            x.UseNpgsql(configuration.GetConnectionString("Postgres"));
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }

   
}