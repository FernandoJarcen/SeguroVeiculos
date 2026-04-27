using Insurance.Application.Services;
using Insurance.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ISeguroService, SeguroService>();
            return services;
        }
    }
}
