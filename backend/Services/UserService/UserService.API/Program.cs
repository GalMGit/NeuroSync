using NeuroSync.MinimalApi.Endpoints;
using UserService.API.DI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddConfiguration(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapEndpoints();

await app.RunAsync();
