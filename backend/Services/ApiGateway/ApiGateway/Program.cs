using ApiGateway.DI;
using ApiGateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendClient", policy =>
    {
        policy.WithOrigins("http://localhost:5206")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddConfiguration(builder.Configuration);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddOcelot("ocelot-configuration", builder.Environment);

builder.Services.AddTransient<DownstreamErrorHandler>();

builder.Services.AddOcelot(builder.Configuration)
    .AddDelegatingHandler<DownstreamErrorHandler>(global: true);

var app = builder.Build();

app.UseCors("FrontendClient");

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

await app.RunAsync();