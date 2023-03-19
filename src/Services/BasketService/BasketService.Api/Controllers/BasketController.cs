using BasketService.Application.Repository;
using BasketService.Application.Services;
using BasketService.Domain;
using EventBus.Contracts.ServiceEvents.Basket;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BasketService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository repository;
        private readonly IIdentityService identityService;
        private readonly IBus bus;
        private readonly ILogger<BasketController> logger;
        public BasketController(
            IBasketRepository repository,
            IIdentityService identityService,
            IBus bus,
            ILogger<BasketController> logger)
        {
            this.repository = repository;
            this.identityService = identityService;
            this.bus = bus;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Basket Service is running right now!");
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(CustomerBasket),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody]CustomerBasket basket)
        {
            return Ok(await repository.UpdateBasketAsync(basket));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            var basket = await repository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        [Route("additem")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddItemToBasket([FromBody] BasketItem basketItem)
        {
            var userId = identityService.GetUserName();

            var basket = await repository.GetBasketAsync(userId);

            if (basket == null)
            {
                basket = new CustomerBasket(userId);
            }

            basket.Items.Add(basketItem);
           
            await repository.UpdateBasketAsync(basket);
            
            return Ok();
        }

        [HttpPost]
        [Route("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout)
        {
            var userId = basketCheckout.Buyer;

            var basket = await repository.GetBasketAsync(userId);

            if (basket is null)
                return BadRequest();

            var userName = identityService.GetUserName();

            var eventMessage = new OrderCreatedIntegrationEvent(
                userId: userId,
                userName: userName,
                orderNumber: basketCheckout.OrderNumber,
                address:  new EventBus.Contracts.Models.Address
                {
                    City = basketCheckout.City,
                    Country = basketCheckout.Country,
                    State = basketCheckout.State,
                    Street = basketCheckout.Street,
                    ZipCode = basketCheckout.ZipCode,
                },
                cardInformation:new EventBus.Contracts.Models.CardInformation
                {
                    CardExpiration = basketCheckout.CardExpiration,
                    CardHolderName = basketCheckout.CardHolderName,
                    CardNumber = basketCheckout.CardNumber,
                    CardSecurityNumber = basketCheckout.CardSecurityNumber,
                    CardTypeId = basketCheckout.CardTypeId,
                },
                buyer: basketCheckout.Buyer
            );

            await bus.Publish<OrderCreatedIntegrationEvent>(eventMessage);

            return Accepted();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        public async Task<bool> DeleteBasketByIdAsync(string Id)
        {
            return await repository.DeleteBasketAsync(Id);
        }
    }
}
