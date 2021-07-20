using Newtonsoft.Json;

namespace CryptoCurrency.ExchangeClient.Kraken.Model
{
    public class KrakenAsset
    {
        [JsonProperty(PropertyName = "aclass")]
        public string Aclass { get; set; }

        [JsonProperty(PropertyName = "altname")]
        public string AltName { get; set; }

        [JsonProperty(PropertyName = "decimals")]
        public int Decimals { get; set; }

        [JsonProperty(Propert