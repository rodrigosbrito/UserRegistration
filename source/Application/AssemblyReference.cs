using Application.Behaviors;
using Application.User.RegisterUser;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(AssemblyReference).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(typeof(RegisterUserCommandValidator).Assembly);

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingPipelineBehavior<,>));

            return services;

        }
    }
}
