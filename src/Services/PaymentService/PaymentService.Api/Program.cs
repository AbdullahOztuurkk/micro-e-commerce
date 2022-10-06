using EventBus.Contracts.Order;
using EventBus.MassTransit.RabbitMq.Constants;
using MassTransit;
using PaymentService.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderStartedIntegrationEventHandler>();

    x.SetKebabCaseEndpointNameFormatter();
    
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.ConfigureEndpoints(provider);

        cfg.Host(RabbitMqConstants.Uri, h =>
        {
            h.Username(RabbitMqConstants.UserName);
            h.Password(RabbitMqConstants.Password);
        });
        cfg.ReceiveEndpoint(RabbitMqConstants.OrderServiceQueueName, ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 10));
            ep.UseRateLimit(1000, TimeSpan.FromMinutes(1));

            ep.ConfigureConsumer<OrderStartedIntegrationEventHandler>(provider);
        });
    }));
});

var app = builder.Build();

var bus = app.Services.GetRequiredService<IBus>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
