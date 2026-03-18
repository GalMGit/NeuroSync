using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostService.BLL.Consumers;
using PostService.BLL.Mappers;
using PostService.BLL.Services.Commands;
using PostService.BLL.Services.Events;
using PostService.BLL.Services.Queries;
using PostService.CORE.Interfaces.IRepositories;
using PostService.CORE.Interfaces.IServices;
using PostService.CORE.Interfaces.IServices.ICommands;
using PostService.CORE.Interfaces.IServices.IEvents;
using PostService.CORE.Interfaces.IServices.IQueries;
using PostService.DAL.Database.DbFactory;
using PostService.DAL.Repositories;
using Shared.AuthExtensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserDeletedConsumer>();
    x.AddConsumer<UserRestoredConsumer>();
    x.AddConsumer<CommunityDeletedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("post-user-deleted-queue", e =>
        {
            e.ConfigureConsumer<UserDeletedConsumer>(context);
        });

        cfg.ReceiveEndpoint("post-user-restored-queue", e =>
        {
            e.ConfigureConsumer<UserRestoredConsumer>(context);
        });

        cfg.ReceiveEndpoint("post-community-deleted-queue", e =>
        {
            e.ConfigureConsumer<CommunityDeletedConsumer>(context);
        });
    });
});

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(PostMappingProfile).Assembly);

builder.Services.AddSingleton(_ => new DbFactory(
    builder.Configuration["Database:ConnectionString"],
    builder.Configuration["Database:DatabaseName"]
));

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostEventService, PostEventService>();
builder.Services.AddScoped<IPostQueryService, PostQueryService>();
builder.Services.AddScoped<IPostCommandService,PostCommandService>();
builder.Services.AddScoped<IPostService, PostService.BLL.Services.PostService>();
builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();