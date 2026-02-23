using NeuroSync.MinimalApi.Endpoints;
using Shared.AuthExtensions;
using UserService.API.DI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddAuth(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
