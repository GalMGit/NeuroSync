using AuthService.API.Extensions;
using AuthService.BLL.Mappers;
using AuthService.BLL.Services;
using AuthService.BLL.Services.Auth;
using AuthService.BLL.Services.Commands;
using AuthService.BLL.Services.IEvents;
using AuthService.BLL.Services.Queries;
using AuthService.CORE.Interfaces.IRepositories;
using AuthService.CORE.Interfaces.IServices;
using AuthService.CORE.Interfaces.IServices.IAuth;
using AuthService.CORE.Interfaces.IServices.ICommands;
using AuthService.CORE.Interfaces.IServices.IEvents;
using AuthService.CORE.Interfaces.IServices.IQueries;
using AuthService.DAL.Database.DatabaseContext;
using AuthService.DAL.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NeuroSync.MinimalApi.Endpoints;
using Shared.AuthExtensions;

namespace AuthService.API.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddSwaggerGen();
        services.AddEndpoints(typeof(Program).Assembly);
        services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
        services.AddServices(configuration);
        services.AddAuth(configuration);

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
        services.AddScoped<IUserEventService, UserEventService>();
        services.AddScoped<IUserCommandService, UserCommandService>();
        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }


}