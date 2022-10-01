using EventBus.MassTransit.RabbitMq.Constants;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.IntegrationEvents.Handlers;

namespace MyNamespace;

public class Program
{
    public static void Main(string[] args)
    {
        ServiceCollection services = new();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderPaymentFailedIntegrationEventHandler>();
            x.AddConsumer<OrderPaymentSuccessIntegrationEventHandler>();

            x.SetKebabCaseEndpointNameFormatter();

            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ConfigureEndpoints(provider);

                cfg.Host(RabbitMqConstants.Uri, h =>
                {
                    h.Username(RabbitMqConstants.UserName);
                    h.Password(RabbitMqConstants.Password);
                });
                cfg.ReceiveEndpoint(RabbitMqConstants.PaymentServiceQueueName, ep =>
                {
                    ep.ConfigureConsumer<OrderPaymentFailedIntegrationEventHandler>(provider);
                    ep.ConfigureConsumer<OrderPaymentSuccessIntegrationEventHandler>(provider);

                });
            }));
        });

        var serviceProvider = services.BuildServiceProvider();

        var bus = serviceProvider.GetRequiredService<IBus>();

       
        Console.WriteLine("Notification Service is running!");

        Console.ReadLine();
    }
}



