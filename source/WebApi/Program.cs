using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Registration API", Version = "v1" });
});

var configuration = builder.Configuration;

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("api/health", async context =>
        await context.Response.WriteAsync($"My API is healthy in {app.Environment.EnvironmentName} stage!"))
   .WithName("HealthCheck")
   .WithDisplayName("HealthCheck");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

