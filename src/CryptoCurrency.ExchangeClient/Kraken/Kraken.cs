
﻿using System.Collections.Generic;
using System.Threading.Tasks;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.Extensions;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Exchange.Model;
using CryptoCurrency.Core.OrderState;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.OrderType;
using CryptoCurrency.Core.Symbol;

using CryptoCurrency.ExchangeClient.Kraken.Model;

namespace CryptoCurrency.ExchangeClient.Kraken
{
    public class Kraken : IExchange
    {
        private ICurrencyFactory CurrencyFactory { get; set; }

        private ISymbolFactory SymbolFactory { get; set; }

        public Kraken(ICurrencyFactory currencyFactory, ISymbolFactory symbolFactory)
        {
            CurrencyFactory = currencyFactory;
            SymbolFactory = symbolFactory;
        }

        public ExchangeEnum Name => ExchangeEnum.Kraken;

        public ICollection<ExchangeCurrencyConverter> CurrencyConverter
        {
            get
            {
                return new List<ExchangeCurrencyConverter>()
                {
                    new ExchangeCurrencyConverter() { CurrencyCode = CurrencyCodeEnum.BTC, AltCurrencyCode = "XBT" }
                };
            }
        }

        public ICollection<SymbolCodeEnum> Symbol
        {
            get
            {
                return new List<SymbolCodeEnum>
                {
                    SymbolCodeEnum.BTCUSD,
                    SymbolCodeEnum.ETHUSD,
                    SymbolCodeEnum.LTCUSD,
                    SymbolCodeEnum.ETHBTC,
                    SymbolCodeEnum.LTCBTC
                };
            }
        }

        public ICollection<ExchangeStatsKeyEnum> SupportedStatKeys => null;

        public bool SupportsHistoricalLoad => true;

        public bool Initialized { get; private set; }

        public async Task Initialize()
        {
            var assetsResponse = await HttpProxy.GetJson<KrakenWrappedResponse<Dictionary<string, KrakenAsset>>>(GetHttpClient().GetFullUrl("0/public/Assets"), null);

            Assets = assetsResponse.Result;

            var assetPairsResponse = await HttpProxy.GetJson<KrakenWrappedResponse<Dictionary<string, KrakenAssetPair>>>(GetHttpClient().GetFullUrl("0/public/AssetPairs"), null);

            AssetPairs = assetPairsResponse.Result;

            Initialized = true;
        }

        public IExchangeHttpClient GetHttpClient()
        {
            return new Http.Client(this, CurrencyFactory, SymbolFactory);
        }

        public IExchangeWebSocketClient GetWebSocketClient()
        {
            return new WebSocket.Client(this, CurrencyFactory, SymbolFactory);
        }

        #region Custom functionality
        public Dictionary<string, KrakenAsset> Assets { get; private set; }

        public Dictionary<string, KrakenAssetPair> AssetPairs { get; private set; }

        public CurrencyCodeEnum[] DecodeQuotePair(string pair)
        {
            return new CurrencyCodeEnum[2]
            {
                this.GetStandardisedCurrencyCode(CurrencyFactory, pair.Substring(1, 3)),
                this.GetStandardisedCurrencyCode(CurrencyFactory, pair.Substring(5, 3))
            };
        }

        public CurrencyCodeEnum[] DecodeAssetPair(string pair)
        {
            return new CurrencyCodeEnum[2]
            {
                this.GetStandardisedCurrencyCode(CurrencyFactory, pair.Substring(0, 3)),
                this.GetStandardisedCurrencyCode(CurrencyFactory, pair.Substring(3, 3))
            };
        }

        public OrderStateEnum GetOrderState(string status)
        {
            switch (status)
            {
                case "pending":
                    return OrderStateEnum.Pending;
                case "open":
                    return OrderStateEnum.Processing;
                case "closed":
                    return OrderStateEnum.Complete;
                case "canceled":
                    return OrderStateEnum.Cancelled;
                default:
                    return OrderStateEnum.Cancelled;
            }
        }

        public OrderSideEnum GetOrderSide(string side)
        {
            return side == "buy" ? OrderSideEnum.Buy : OrderSideEnum.Sell;
        }

        public OrderTypeEnum GetOrderType(string type)
        {
            return type == "market" ? OrderTypeEnum.Market : OrderTypeEnum.Limit;
        }
        #endregion
    }
}