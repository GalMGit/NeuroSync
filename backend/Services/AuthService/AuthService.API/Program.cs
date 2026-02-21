using AuthService.API.DI;
using NeuroSync.MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();


app.MapEndpoints();

await  app.RunAsync();
