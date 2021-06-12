using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Exchange.Model;
using CryptoCurrency.Core.Extensions;
using CryptoCurrency.Core.Market;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.OrderType;
using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.RateLimiter;

using CryptoCurrency.ExchangeClient.Kraken.Model;

namespace CryptoCurrency.ExchangeClient.Kraken.Http
{
    public class Client : IExchangeHttpClient
    {
        private Kraken Exchange { get; set; }
        private ICurrencyFactory CurrencyFactory { get; set; }
        private ISymbolFactory SymbolFactory { get; set; }

        public IRateLimiter RateLimiter { get; set; }

        public Client(Kraken exchange, ICurrencyFactory currencyFactory, ISymbolFactory symbolFactory)
        {
            Exchange = exchange;
            CurrencyFactory = currencyFactory;
            SymbolFactory = symbolFactory;

            RateLimiter = new BinanceRateLimiter();
        }

        public string ApiUrl => "https://api.kraken.com";

        public bool MultiTickSupported => true;

        public string InitialTradeFilter => "0";

        public async Task<WrappedResponse<CancelOrder>> CancelOrder(ISymbol symbol, string[] orderIds)
        {
            var relativeUrl = "CancelOrder";

            var nvc = new NameValueCollection();
            nvc.Add("txid", orderIds.First());

            return await InternalRequest<KrakenCancelOrderResult, CancelOrder>(true, relativeUrl, HttpMethod.Post, nvc);
        }

        public async Task<WrappedResponse<CreateOrder>> CreateOrder(ISymbol symbol, OrderTypeEnum orderType, OrderSideEnum orderSide, deci