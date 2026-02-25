using CommunityService.BLL.Mappers;
using CommunityService.CORE.Interfaces.IRepositories;
using CommunityService.CORE.Interfaces.IServices;
using CommunityService.DAL.Database.DatabaseContext;
using CommunityService.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.AuthExtensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(CommunityMappingProfile).Assembly);
builder.Services.AddDbContext<CommunityDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
builder.Services.AddScoped<ICommunityService, CommunityService.BLL.Services.CommunityService>();

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