using Domain.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.AuthUser;
using Infrastructure.Context;
using Infrastructure.Jwt;
using Infrastructure.Security;
using Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}
