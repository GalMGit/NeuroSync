using ApiGateway.DI;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConfiguration(builder.Configuration);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapMiddlewares();

await app.UseOcelot();

app.Run();
