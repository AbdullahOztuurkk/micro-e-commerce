using EventBus.MassTransit.RabbitMq;
using MassTransit;
using PaymentService.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransitAsEventBus((bus) =>
{
    bus.AddConsumer<OrderStartedIntegrationEventHandler>();
},
(factory, provider) =>
{
    factory.ReceiveEndpoint(Global.Queues.PaymentService.OrderStartedIntegrationEvent, ep =>
    {
        ep.ConfigureConsumer<OrderStartedIntegrationEventHandler>(provider);
    });
});

var app = builder.Build();

var bus = app.Services.GetRequiredService<IBusControl>();

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

bus.StartAsync();

Console.Out.WriteLine("Payment service is working right now!");

bus.StopAsync();