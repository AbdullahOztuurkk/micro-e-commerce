using EventBus.MassTransit.RabbitMq;
using MassTransit;
using OrderService.Api.Extensions;
using OrderService.Api.IntegrationEventHandlers;
using OrderService.Application;
using OrderService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceRegistration(builder.Configuration);

builder.Services.AddMassTransitAsEventBus((bus) =>
{
    bus.AddConsumer<OrderStartedIntegrationEventHandler>();
},
(factory, provider) =>
{
    factory.ReceiveEndpoint(Global.Queues.Order_OrderStarted, ep =>
    {
        ep.ConfigureConsumer<OrderStartedIntegrationEventHandler>(provider);
    });
});

var app = builder.Build();

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
