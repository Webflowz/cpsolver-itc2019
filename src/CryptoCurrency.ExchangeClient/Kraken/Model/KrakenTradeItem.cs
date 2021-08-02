using System.Collections.Generic;

using Newtonsoft.Json;

namespace CryptoCurrency.ExchangeClient.Kraken.Model
{
    public class KrakenTradeHistory
    {
        [JsonProperty(PropertyName = "trades")]
        public Dictionary<string, KrakenTradeItem> Trades { get; set; }

        [JsonPrope