using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.CheckoutService.Interface
{
    public class CheckoutSummary
    {
        public List<CheckoutProduct> Products { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
