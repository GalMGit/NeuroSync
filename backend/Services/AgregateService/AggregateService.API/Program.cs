using AggregateService.API.Extensions;
using AggregateService.API.Services.Interfaces;
using AggregateService.API.Services.Realisations;
using Shared.AuthExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<TokenPropagationHandler>();
builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:UserService"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
}).AddHttpMessageHandler<TokenPropagationHandler>();

builder.Services.AddHttpClient("PostService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PostService"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
}).AddHttpMessageHandler<TokenPropagationHandler>();

builder.Services.AddHttpClient("CommentService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:CommentService"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
}).AddHttpMessageHandler<TokenPropagationHandler>();

builder.Services.AddHttpClient("CommunityService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:CommunityService"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
}).AddHttpMessageHandler<TokenPropagationHandler>();



builder.Services.AddAuth(builder.Configuration);

builder.Services.AddScoped<IUserServiceClient, UserServiceClient>();
builder.Services.AddScoped<IPostServiceClient, PostServiceClient>();
builder.Services.AddScoped<ICommentServiceClient, CommentServiceClient>();
builder.Services.AddScoped<ICommunityServiceClient, CommunityServiceClient>();


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();