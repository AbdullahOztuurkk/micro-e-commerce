using BasketService.Application.Repository;
using BasketService.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.ConfigureAuth(configuration);
            services.AddSingleton(sp => sp.ConfigureRedis(configuration));

            services.AddHttpContextAccessor();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<IBasketRepository, RedisBasketRepository>();
        }
    }
}
