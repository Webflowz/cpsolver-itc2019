using System;

using Newtonsoft.Json;

namespace CryptoCurrency.ExchangeClient.CoinbasePro.Model
{
    public class CoinbaseProTick
    {
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "bid")]
        public decimal Bid { get; set; }

        [JsonProperty(PropertyName = "ask")]
        public decimal Ask { get; set; }

        [JsonProperty(PropertyName = "volum