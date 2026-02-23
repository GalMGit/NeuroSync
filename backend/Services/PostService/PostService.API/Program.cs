using Microsoft.EntityFrameworkCore;
using PostService.BLL.Mappers;
using PostService.CORE.Interfaces.IRepositories;
using PostService.CORE.Interfaces.IServices;
using PostService.DAL.Database.DatabaseContext;
using PostService.DAL.Repositories;
using Shared.AuthExtensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(PostMappingProfile).Assembly);
builder.Services.AddDbContext<PostDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddScoped<IPostRepository, PostRepository>();
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