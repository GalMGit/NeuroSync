using AuthService.API.DI;
using MassTransit;
using NeuroSync.MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
});
builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapEndpoints();

await app.RunAsync();
