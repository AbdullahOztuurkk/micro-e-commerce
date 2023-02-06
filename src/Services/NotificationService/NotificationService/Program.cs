using EventBus.MassTransit.RabbitMq;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Handlers;

namespace MyNamespace;

public class Program
{
    public static void Main(string[] args)
    {
        ServiceCollection services = new();

        services.AddMassTransitAsEventBus((bus) =>
        {
            bus.AddConsumer<OrderPaymentFailedIntegrationEventHandler>();
            bus.AddConsumer<OrderPaymentSuccessIntegrationEventHandler>();
        }, 
        (factory, provider) =>
        {
            factory.ReceiveEndpoint(Global.Queues.OrderPaymentSuccessIntegrationEvent, ep =>
            {
                ep.ConfigureConsumer<OrderPaymentSuccessIntegrationEventHandler>(provider);
            });

            factory.ReceiveEndpoint(Global.Queues.OrderPaymentFailedIntegrationEvent, ep =>
            {
                ep.ConfigureConsumer<OrderPaymentFailedIntegrationEventHandler>(provider);
            });
        });

        var serviceProvider = services.BuildServiceProvider();

        var bus = serviceProvider.GetRequiredService<IBusControl>();

        bus.StartAsync();

        Console.Out.WriteLine("Notification Service is working right now!");
        
        bus.StopAsync();
    }
}



