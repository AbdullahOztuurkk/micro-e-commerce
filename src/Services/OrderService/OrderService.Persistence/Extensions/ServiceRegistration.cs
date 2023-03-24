using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Contracts.Repositories;
using OrderService.Infrastructure.Context;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(opt =>
            {
                opt.UseSqlServer(configuration["OrderDbConnectionString"]);
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();

            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>().UseSqlServer("OrderDbConnectionString");

            using var dbContext = new OrderDbContext(optionsBuilder.Options, null);

            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();

            return services;
        }
    }
}
