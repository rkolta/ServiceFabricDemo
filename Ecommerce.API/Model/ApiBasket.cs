﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Model
{
    public class ApiBasket
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("items")]
        public ApiBasketItem[] Items { get; set; }

    }
    public class ApiBasketItem
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}