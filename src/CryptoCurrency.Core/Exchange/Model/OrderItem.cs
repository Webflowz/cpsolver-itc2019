using System;

using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.OrderState;
using CryptoCurrency.Core.OrderType;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.Core.Exchange.Model
{
    public class OrderItem
    {
        public ExchangeEnum Exchange { get; set; }

        public SymbolCodeEnu