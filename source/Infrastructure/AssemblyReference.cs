using Domain.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.AuthUser;
using Infrastructure.Context;
using Infrastructure.Jwt;
using Infrastructure.Security;
using Infrastructure.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UserDatabase"))
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors());

            services.BuildServiceProvider().GetService<ApplicationDbContext>()
                .Database.EnsureCreated();            

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();

            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddSingleton<ICryptographyService, CryptographyService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddAuthorization();

            return services;
        }

        public static void UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration) 
        {
            app.UseSerilogRequestLogging();
            
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
