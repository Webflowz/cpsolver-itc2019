﻿using Newtonsoft.Json;

using System.Collections.Generic;

namespace CryptoCurrency.ExchangeClient.Binance.Model
{
    public class BinanceNewOrderFill
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

        [JsonProperty("commission")]
        public string Commission { get; set; }

        [JsonProperty("commissionAsset")]
        public string CommissionAsset { get; set; }
    }

    public class BinanceNewOrder
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("orderId")]
        public string OrderId { get; set; }
        
        [JsonProperty("clientOrderId")]
        public string ClientOrderId { get; set; }
        
        [JsonProperty("transactTime")]
        public long TransactTime { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("origQty")]
        public decimal OriginalQuantity { get; set; }
        
        [JsonProperty("executedQty")]
        public decimal ExecutedQuantity { get; set; }

        [JsonProperty("cummulativeQuoteQty")]
        public decimal CumulativeQuoteQuantity { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }

        [JsonProperty("type")]
        public s