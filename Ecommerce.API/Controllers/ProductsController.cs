using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Model;
using ECommerce.ProductCatalog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCatalogService _service;
        public ProductsController()
        {
            var proxyFactory = new ServiceProxyFactory(
                c => new FabricTransportServiceRemotingClientFactory()
                );

            _service = proxyFactory.CreateServiceProxy<IProductCatalogService>(
                new Uri("fabric:/ECommerce/Ecommerce.ProductCatalog"),
                new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<ApiProduct>> GetAsync()
        {
            var products = await _service.GetAllProductsAsync();
            return products.Select(p => new ApiProduct
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsAvailable = p.Availability > 0
            });
        }

        [HttpPost]
        public async Task PostAsync([FromBody] ApiProduct apiProduct)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = apiProduct.Name,
                Description = apiProduct.Description,
                Price = apiProduct.Price,
                Availability = 100
            };

            await _service.AddProductAsync(product);
        }
    }
}
