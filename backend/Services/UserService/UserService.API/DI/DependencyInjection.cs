using MassTransit;
using Microsoft.EntityFrameworkCore;
using NeuroSync.MinimalApi.Endpoints;
using UserService.BLL.Consumers;
using UserService.BLL.Mappers;
using UserService.CORE.Interfaces.IRepositories;
using UserService.CORE.Interfaces.IServices;
using UserService.DAL.Database.DatabaseContext;
using UserService.DAL.Repositories;

namespace UserService.API.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddRabbitMq();
        services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
        services.AddServices(configuration);
        services.AddEndpoints(typeof(Program).Assembly);
        
        return services;
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(x =>
        {
            x.UseNpgsql(configuration.GetConnectionString("Postgres"));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, BLL.Services.UserService>();
        return services;
    }

    private static IServiceCollection AddRabbitMq(
        this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ReceiveEndpoint("user-created-queue", e =>
                {
                    e.ConfigureConsumer<UserCreatedConsumer>(context);
                });
            });
        });
        return services;
    }
}