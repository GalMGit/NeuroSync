using ApiGateway.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapMiddlewares();
app.MapReverseProxy();


app.Run();
