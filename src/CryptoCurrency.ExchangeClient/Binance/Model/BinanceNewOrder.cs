using Newtonsoft.Json;

using System.Collections.Generic;

namespace CryptoCurrency.ExchangeClient.Binance.Model
{
    public class BinanceNewOrderFill
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

        [JsonPropert