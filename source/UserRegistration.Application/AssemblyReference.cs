using UserRegistration.Application.Behaviors;
using UserRegistration.Application.User.RegisterUser;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UserRegistration.Application
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
