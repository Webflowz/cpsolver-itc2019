using Newtonsoft.Json;

namespace CryptoCurrency.ExchangeClient.Binance.Model
{
    public class BinanceOpenOrder
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("clientOrderId")]
        public string ClientOrderId { get; set; }
        
        [JsonProperty("price")]
        public dec