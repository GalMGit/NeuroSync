using AuthService.API.DI;
using NeuroSync.MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapEndpoints();

await  app.RunAsync();
