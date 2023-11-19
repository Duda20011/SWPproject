using Microsoft.Extensions.DependencyInjection;
using Services.Service;
using Services.Service.Interface;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection CoreServices(this IServiceCollection services)
        {

            return services;
        }
    }
}
