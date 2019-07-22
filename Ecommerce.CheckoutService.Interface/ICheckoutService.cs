using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Threading.Tasks;

namespace Ecommerce.CheckoutService.Interface
{
    public interface ICheckoutService : IService
    {
        Task<CheckoutSummary> CheckoutAsync(string userId);
        Task<CheckoutSummary[]> GetOrderHistoryAsync(string userId);

    }
}
