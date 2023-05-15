using UserRegistration.Domain.Interfaces;
using UserRegistration.Infrastructure.Authentication;
using UserRegistration.Infrastructure.AuthUser;
using UserRegistration.Infrastructure.Context;
using UserRegistration.Infrastructure.Jwt;
using UserRegistration.Infrastructure.Security;
using UserRegistration.Infrastructure.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace UserRegistration.Infrastructure
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

            services.AddSingleton<ICryptographyService, CryptographyService>();

            return services;
        }

        public static void UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration) 
        {
            app.UseSerilogRequestLogging();
        }
    }
}
