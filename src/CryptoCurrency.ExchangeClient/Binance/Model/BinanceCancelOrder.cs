using Newtonsoft.Json;

namespace CryptoCurrency.ExchangeClient.Binance.Model
{
    public class BinanceCancelOrder
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("origClientOrderId")]
        public string OrigClientOrderId { get; set; }

        [JsonProperty("clientOrderId")]
        public string ClientOrderId { get; set; }

        [JsonProperty("transactTime")]
        public long TransactTime { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timeInForce")]
     