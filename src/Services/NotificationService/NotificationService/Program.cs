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
            bus.SetKebabCaseEndpointNameFormatter();
            bus.AddConsumer<OrderPaymentFailedIntegrationEventHandler>();
            bus.AddConsumer<OrderPaymentSuccessIntegrationEventHandler>();
        }, (factory, provider) =>
        {
            factory.ReceiveEndpoint(Global.Services.PaymentServiceQueueName, ep =>
            {
                ep.ConfigureConsumer<OrderPaymentFailedIntegrationEventHandler>(provider);
                ep.ConfigureConsumer<OrderPaymentSuccessIntegrationEventHandler>(provider);
            });
        });

        var serviceProvider = services.BuildServiceProvider();

        var bus = serviceProvider.GetRequiredService<IBus>();

        Console.WriteLine("Notification Service is running!");

        Console.ReadLine();
    }
}



