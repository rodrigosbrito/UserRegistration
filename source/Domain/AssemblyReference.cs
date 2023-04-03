using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services;
        }
    }
}
