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

                var bookTicks = (obj as ICollection<BinanceOrderBookTicker>);

                foreach(var tick in bookTicks)
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
                        BuyPrice = tick.AskPrice,
                        SellPrice = tick.BidPrice
                    });
                }

                return (T2)(object)ticks;
            }

            if (typeof(T2) == typeof(TradeResult))
            {
                var trades = obj as ICollection<Dictionary<string, object>>;

                var filter = trades.Count > 0 ? trades.Last()["a"].ToString() : postData["fromId"];

                var binanceSymbol = exchange.Info.Symbols.Where(x => x.Symbol == postData["symbol"]).FirstOrDefault();

                var baseCurrencyCode = exchange.GetStandardisedCurrencyCode(currencyFactory, binanceSymbol.BaseAsset);
                var quoteCurrencyCode = exchange.GetStandardisedCurrencyCode(currencyFactory, binanceSymbol.QuoteAsset);
                var symbol = symbolFactory.Get(baseCurrencyCode, quoteCurrencyCode);

                return (T2)(object)new TradeResult
                {
                    Exchange = exchange.Name,
                    SymbolCode = symbol.Code,                    
                    Trades = trades.Select(t => new MarketTrade
                    {
                        Exchange = exchange.Name,
                        SymbolCode = symbol.Code,
                        Epoch = Epoch.FromMilliseconds(Convert.ToInt64(t["T"])),
                        Price = Convert.ToDecimal(t["p"]),
                        Volume = Convert.ToDecimal(t["q"]),
                        Side = Convert.ToBoolean(t["m"]) ? OrderSideEnum.Sell : OrderSideEnum.Buy,
                        SourceTradeId = Convert.ToString(t["a"])
                    }).ToList(),
                    Filter = filter
                };
            }

            if(typeof(T) == typeof(ICollection<BinanceTradeItem>))
            {
                var binanceSymbol = exchange.Info.Symbols.Where(x => x.Symbol == postData["symbol"]).FirstOrDefault();

                var baseCurrencyCode = exchange.GetStandardisedCurrencyCode(currencyFactory, binanceSymbol.BaseAsset);
                var quoteCurrencyCode = exchange.GetStandardisedCurrencyCode(currencyFactory, binanceSymbol.QuoteAsset);

                var symbol = symbolFactory.Get(baseCurrencyCode, quoteCurrencyCode);

                var trades = obj as ICollection<BinanceTradeItem>;

                return (T2)(object)trades.Select(t => new TradeItem
                {
                    Exchange = exchange.Name,
                    SymbolCode = symbol.Code,
                    Created = Epoch.FromMilliseconds(t.Time),
                    Id = t.Id.ToString(),
                    OrderId = 