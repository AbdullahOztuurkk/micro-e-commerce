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

    [HttpPost]
    public async Task<IActionResult> CreateFakePaymentAsync()
    {
        await bus.Publish(new OrderPaymentFailedIntegrationEvent(Guid.NewGuid(),new Random().Next(0, 100).ToString()));
        return Ok();
    }
}
