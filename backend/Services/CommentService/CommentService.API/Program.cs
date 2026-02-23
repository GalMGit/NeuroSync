using CommentService.BLL.Mappers;
using CommentService.CORE.Interfaces.IRepositories;
using CommentService.CORE.Interfaces.IServices;
using CommentService.DAL.Database.DatabaseContext;
using CommentService.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.AuthExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(CommentMappingProfile).Assembly);
builder.Services.AddDbContext<CommentDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
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