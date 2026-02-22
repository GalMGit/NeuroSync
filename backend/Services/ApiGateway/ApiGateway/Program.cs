
using System.Security.Claims;
using ApiGateway.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.User?.Identity?.IsAuthenticated == true)
    {
        var userId = context.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var username = context.User
            .FindFirst(ClaimTypes.Name)?.Value;

        if (!string.IsNullOrEmpty(userId))
            context.Request.Headers["X-User-Id"] = userId;

        if (!string.IsNullOrEmpty(username))
            context.Request.Headers["X-Username"] = username;
    }

    await next();
});
app.MapReverseProxy();


app.Run();
