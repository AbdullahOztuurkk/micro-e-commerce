using CatalogService.Application.Profiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CatalogService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CatalogProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
