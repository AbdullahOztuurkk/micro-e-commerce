using BasketService.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.Repository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string customerId);
        IEnumerable<string> GetUsers();
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string Id);
    }

    public class RedisBasketRepository : IBasketRepository
    {
        private readonly ILogger<RedisBasketRepository> logger;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase database;

        public RedisBasketRepository(ILoggerFactory loggerFactory, IConnectionMultiplexer redis)
        {
            this.logger = loggerFactory.CreateLogger<RedisBasketRepository>();
            _redis = redis;
            database = _redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var data = await database.StringGetAsync(customerId);
            return !data.IsNullOrEmpty ? JsonConvert.DeserializeObject<CustomerBasket>(data) : null;
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();
            return data?.Select(x => x.ToString());
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));

            if (created)
            {
                logger.LogInformation($"Update Basket Event has error occurred!");
            }

            logger.LogInformation($"Updated Basket By Buyer Id : {basket.BuyerId} at {DateTime.UtcNow}");
            
            return await GetBasketAsync(basket.BuyerId);
        }

        #region Private

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

        #endregion
    }
}
