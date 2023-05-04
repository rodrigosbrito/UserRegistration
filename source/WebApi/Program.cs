using Application;
using Application.Behaviors;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Configuration;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Registration API", Version = "v1" });
});
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsSetup>();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
builder.Services.AddScoped(
    typeof(IPipelineBehavior<,>), 
    typeof(LoggingPipelineBehavior<,>));

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthorization();

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

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();

