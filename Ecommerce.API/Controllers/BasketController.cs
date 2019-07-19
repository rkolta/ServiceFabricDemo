using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using UserActor.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<ApiBasket> GetAsync(string userId)
        {
            IUserActor actor = GetActor(userId);

            var products = await actor.GetBasket();
            return new ApiBasket()
            {
                UserId = userId,
                Items = products.Select(
                    p => new ApiBasketItem
                    {
                        ProductId = p.ProductId.ToString(),
                        Quantity = p.Quantity
                    })
                    .ToArray()
            };
        }

        [HttpPost("{UserId}")]
        public async Task AddAsync(string userId, [FromBody] ApiBasketAddRequest request)
        {
            IUserActor actor = GetActor(userId);
            await actor.AddToBasket(request.ProductId, request.Quantity);
        }

        [HttpDelete("userId")]
        public async Task DeleteAsync(string userId)
        {
            IUserActor actor = GetActor(userId);
            await actor.ClearBasket();

        }

        private IUserActor GetActor(string userId)
        {
            return ActorProxy.Create<IUserActor>(
                new ActorId(userId),
                new Uri("fabric:/ECommerce/UserActorService"));
        }

    }
}
