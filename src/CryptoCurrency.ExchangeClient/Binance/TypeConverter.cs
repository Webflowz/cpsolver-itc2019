using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.Exchange.Model;
using CryptoCurrency.Core.Extensions;
using CryptoCurrency.Core.Market;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.OrderType;
using CryptoCurrency.Core.Symbol;

using CryptoCurrency.ExchangeClient.Binance.Model;

namespace CryptoCurrency.ExchangeClient.Binance
{
    public static class TypeConverter
    {
        public static T2 ChangeType<T, T2>(this Binance exchange, ICurrencyFactory currencyFactory, ISymbolFactory symbolFactory, NameValueCollection postData, T obj)
        {
            if (typeof(T) == typeof(ICollection<BinancePriceTicker>))
            {
                var ticks = new List<MarketTick>();

                var priceTicks = obj as ICollection<BinancePriceTicker>;

                foreach(var tick in priceTicks)
                {
                    var binanceSymbol = exchange.Info.Symbols.Where(x => x.Symbol == tick.Symbol).FirstOrDefault();

                    var baseCurrencyCode = exchange.GetStandardisedCurrencyCode(currencyFactory, binanceSymbol.BaseAsset);
                    var quoteCurrencyCode = exchange.GetStandardisedCurrencyCode(currencyFactory, binanceSymbol.QuoteAsset);
                    var symbol = symbolFactory.Get(baseCurrencyCode, quoteCurrencyCode);

                    ticks.Add(new MarketTick
                    {
                        Exchange = exchange.Name,
                        SymbolCode = symbol.Code,
                        Epoch = Epoch.Now,
                        LastPrice = tick.Price,
                    });
                }

                return (T2)(object)ticks;
            }

            if (typeof(T) == typeof(ICollection<BinanceOrderBookTicker>))
            {
                var ticks = new List<MarketTick>();

                var bookTicks = (obj as ICollection<Binance