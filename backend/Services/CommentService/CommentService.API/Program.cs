using CommentService.BLL.Consumers;
using CommentService.BLL.Mappers;
using CommentService.BLL.Services.Commands;
using CommentService.BLL.Services.Events;
using CommentService.BLL.Services.Queries;
using CommentService.CORE.Interfaces.IRepositories;
using CommentService.CORE.Interfaces.IServices;
using CommentService.CORE.Interfaces.IServices.ICommands;
using CommentService.CORE.Interfaces.IServices.IEvents;
using CommentService.CORE.Interfaces.IServices.IQueries;
using CommentService.DAL.Database.DatabaseContext;
using CommentService.DAL.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.AuthExtensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserDeletedConsumer>();
    x.AddConsumer<UserRestoredConsumer>();
    x.AddConsumer<PostsDeletedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("comment-user-deleted-queue", e =>
        {
            e.ConfigureConsumer<UserDeletedConsumer>(context);
        });

        cfg.ReceiveEndpoint("comment-user-restored-queue", e =>
        {
            e.ConfigureConsumer<UserRestoredConsumer>(context);
        });

        cfg.ReceiveEndpoint("comment-posts-deleted-queue", e =>
        {
            e.ConfigureConsumer<PostsDeletedConsumer>(context);
        });

    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(CommentMappingProfile).Assembly);
builder.Services.AddDbContext<CommentDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentEventService, CommentEventService>();
builder.Services.AddScoped<ICommentQueryService, CommentQueryService>();
builder.Services.AddScoped<ICommentCommandService, CommentCommandService>();
builder.Services.AddScoped<ICommentService, CommentService.BLL.Services.CommentService>();

builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();