
﻿using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.Core.Exchange.Model
{
    public class TradeItem
    {
        public ExchangeEnum Exchange { get; set; }

        public SymbolCodeEnum SymbolCode { get; set; }

        public string Id { get; set; }

        public OrderSideEnum Side { get; set; }

        public Epoch Created { get; set; }

        public decimal Price { get; set; }

        public decimal Volume { get; set; }

        public decimal Fee { get; set; }

        public CurrencyCodeEnum FeeCurrencyCode { get; set; }

        public string OrderId { get; set; }
    }
}