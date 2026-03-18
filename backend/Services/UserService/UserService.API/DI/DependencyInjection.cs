using MassTransit;
using Microsoft.EntityFrameworkCore;
using NeuroSync.MinimalApi.Endpoints;
using UserService.BLL.Consumers;
using UserService.BLL.Mappers;
using UserService.BLL.Services.Commands;
using UserService.BLL.Services.Queries;
using UserService.CORE.Interfaces.IRepositories;
using UserService.CORE.Interfaces.IServices;
using UserService.CORE.Interfaces.IServices.ICommands;
using UserService.CORE.Interfaces.IServices.IQueries;
using UserService.DAL.Database.DbFactory;
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

        return services;
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddSingleton(_ => new DbFactory(
            configuration["Database:ConnectionString"],
            configuration["Database:DatabaseName"]
        ));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IUserCommandService, UserCommandService>();
        services.AddScoped<IUserService, BLL.Services.UserService>();
        return services;
    }

    private static IServiceCollection AddRabbitMq(
        this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            x.AddConsumer<UserDeletedConsumer>();
            x.AddConsumer<UserRestoredConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ReceiveEndpoint("user-created-queue", e =>
                {
                    e.ConfigureConsumer<UserCreatedConsumer>(context);
                });

                cfg.ReceiveEndpoint("user-deleted-queue", e =>
                {
                    e.ConfigureConsumer<UserDeletedConsumer>(context);
                });

                cfg.ReceiveEndpoint("user-restored-queue", e =>
                {
                    e.ConfigureConsumer<UserRestoredConsumer>(context);
                });
            });
        });
        return services;
    }
}