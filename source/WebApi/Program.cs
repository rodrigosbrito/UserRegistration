using Application;
using Infrastructure;
using Infrastructure.Logging;
using Serilog;
using WebApi.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog(builder.Configuration, builder.Environment);
Log.Information($"Starting {builder.Environment.ApplicationName}");

builder.Services.AddApiConfiguration();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddElasticsearch(builder.Configuration);
builder.Services.AddSwagger(builder.Configuration);

var app = builder.Build();

app.MapGet("api/health", async context =>
        await context.Response.WriteAsync($"My API is healthy in {app.Environment.EnvironmentName} stage!"))
   .WithName("HealthCheck")
   .WithDisplayName("HealthCheck");

app.UseSwaggerDoc();

app.UseInfrastructure(app.Configuration);

app.UseElasticApm(app.Configuration);

app.UseApiConfiguration(app.Environment);

app.MapControllers();

app.Run();