using BasketService.Application.Extensions;
using EventBus.MassTransit.RabbitMq;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddSingleton(sp => sp.ConfigureRedis(builder.Configuration));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Services.AddMassTransitAsEventBus((bus) =>
{
    //bus.AddConsumer<BasketEventHandler>();
}, (factory, provider) =>
{
    factory.ReceiveEndpoint(Global.Queues.BasketServiceQueueName, ep =>
    {
        //ep.ConfigureConsumer<BasketIntegrationEventHandler>(provider);
    });
});
*/

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
