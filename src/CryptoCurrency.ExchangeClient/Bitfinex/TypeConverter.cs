using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using Newtonsoft.Json.Linq;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Exchange.Model;
using CryptoCurrency.Core.Market;
using CryptoCurrency.Core.Symbol;

using CryptoCurrency.ExchangeClient.Bitfinex.Model;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.Exchange;

namespace CryptoCurrency.ExchangeClient.Bitfinex
{
    public static class TypeConverter
    {
        public static T2 ChangeType<T, T2>(this Bitfinex exchange, ISymbolFactory symbolFactory, T obj, NameValueCollection query, NameValueCollection additionalData)
        {
            if (typeof(T2) == typeof(ICollection<MarketTick>))
            {
                var ticks = obj as List<object[]>;
                
                return (T2)(object)ticks.Select(t => new MarketTick
                {
                    Exchange = exchange.Name,
                    Epoch = new Epoch(DateTime.UtcNow),
                    SymbolCode = symbolFactory.Get(exchange.DecodeSymbol(t[0].ToString())[0], exchange.DecodeSymbol(t[0].ToString())[1]).Code,
                    BuyPrice = Convert.ToDecimal(t[1]),
                    SellPrice = Convert.ToDecimal(t[3]),
                    LastPrice = Convert.ToDecimal(t[7])
                }).ToList();
            }

            if (typeof(T2) == typeof(MarketTick))
            {
  