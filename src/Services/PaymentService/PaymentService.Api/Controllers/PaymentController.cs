using EventBus.Contracts.Order;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IBus bus;
    public PaymentController(IBus bus)
    {
        this.bus = bus;
    }

    public IActionResult CreateFakePayment()
    {
        return Ok(bus.Publish<OrderStartedIntegrationEvent>(new OrderStartedIntegrationEvent(new Random().Next(0,100))));
    }
}
