using EventBus.MassTransit.RabbitMq;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationService.Handlers;

namespace MyNamespace;

public class Program
{
    public static void Main(string[] args)
    {
        ServiceCollection services = new();

        var loggerFactory = LoggerFactory.Create(o => o.AddConsole());

        services.AddSingleton<ILoggerFactory>(loggerFactory);

        services.AddMassTransitAsEventBus((bus) =>
        {
            bus.AddConsumer<OrderPaymentFailedIntegrationEventHandler>();
            bus.AddConsumer<OrderPaymentSuccessIntegrationEventHandler>();
        }, 
        (factory, provider) =>
        {
            factory.ReceiveEndpoint(Global.Queues.Notification_OrderPaymentSuccess, ep =>
            {
                ep.ConfigureConsumer<OrderPaymentSuccessIntegrationEventHandler>(provider);
            });

            factory.ReceiveEndpoint(Global.Queues.Notification_OrderPaymentFailed, ep =>
            {
                ep.ConfigureConsumer<OrderPaymentFailedIntegrationEventHandler>(provider);
            });
        });

        var serviceProvider = services.BuildServiceProvider();

        var bus = serviceProvider.GetRequiredService<IBusControl>();

        bus.StartAsync();

        Console.Out.WriteLine("Notification Service is working right now!");
        
        bus.StopAsync();

        Console.ReadLine();
    }
}



