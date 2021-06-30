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

        public async Task<WrappedResponse<CreateOrder>> CreateOrder(ISymbol symbol, OrderTypeEnum orderType, OrderSideEnum orderSide, decimal price, decimal volume)
        {
            Exchange.EnsureSymbol(symbol);

            var relativeUrl = "AddOrder";

            var nvc = new NameValueCollection();
            nvc.Add("pair", $"{Exchange.GetCurrencyCode(symbol.BaseCurrencyCode)}{Exchange.GetCurrencyCode(symbol.QuoteCurrencyCode)}");
            nvc.Add("type", orderSide == OrderSideEnum.Buy ? "buy" : "sell");
            nvc.Add("ordertype", orderType == OrderTypeEnum.Market ? "market" : "limit");
            if (orderType == OrderTypeEnum.Limit)
                nvc.Add("price", price.ToString());
            nvc.Add("volume", volume.ToString());

            return await InternalRequest<KrakenAddOrderResult, CreateOrder>(true, relativeUrl, HttpMethod.Post, nvc);
        }

        public async Task<WrappedResponse<ICollection<AccountBalance>>> GetBalance()
        {
            var relativeUrl = "Balance";

            return await InternalRequest<KrakenAccount, ICollection<AccountBalance>>(true, relativeUrl, HttpMethod.Post, null);
        }

        public async Task<WrappedResponse<ICollection<OrderItem>>> GetOpenOrders(ISymbol symbol, int pageNumber, int pageSize)
        {
            Exchange.EnsureSymbol(symbol);

            var relativeUrl = "OpenOrders";

            var nvc = new NameValueCollection();
            nvc.Add("pair", $"{Exchange.GetCurrencyCode(symbol.BaseCurrencyCode)}{Exchange.GetCurrencyCode(symbol.QuoteCurrencyCode)}");

            return await InternalRequest<KrakenOpenOrders, ICollection<OrderItem>>(true, relativeUrl, HttpMethod.Post, nvc);
        }

        public async Task<WrappedResponse<OrderBook>> GetOrderBook(ISymbol symbol, int buyCount, int sellCount)
        {
            Exchange.EnsureSymbol(symbol);

            var relativeUrl = "Depth";

            var nvc = new NameValueCollection();
            nvc.Add("pair", $"{Exchange.GetCurrencyCode(symbol.BaseCurrencyCode)}{Exchange.GetCurrencyCode(symbol.QuoteCurrencyCode)}");

            return await InternalRequest<KrakenOrderBook, OrderBook>(false, relativeUrl, HttpMethod.Get, nvc);
        }

        public async Task<WrappedResponse<MarketTick>> GetTick(ISymbol symbol)
        {
            var relativeUrl = "Ticker";

            var nvc = new NameValueCollection();
            nvc.Add("pair", $"{Exchange.GetCurrencyCode(symbol.BaseCurrencyCode)}{Exchange.GetCurrencyCode(symbol.QuoteCurrencyCode)}");

            return await InternalRequest<KrakenTick, MarketTick>(false, relativeUrl, HttpMethod.Get, nvc);
        }

        public async Task<WrappedResponse<TradeFee>> GetTradeFee(OrderSideEnum orderSide, ISymbol symbol)
        {
            var relativeUrl = "TradeVolume";

            var nvc = new NameValueCollection();
            nvc.Add("pair", $"{Exchange.GetCurrencyCode(symbol.BaseCurrencyCode)}{Exchange.GetCurrencyCode(symbol.QuoteCurrencyCode)}");
            nvc.Add("fee-info", "true");

            return await InternalRequest<KrakenTradeVolume, TradeFee>(true, relativeUrl, HttpMethod.Post, nvc);
        }

        public Task<WrappedResponse<WithdrawCrypto>> WithdrawCrypto(CurrencyCodeEnum cryptoCurrencyCode, decimal withdrawalFee, decimal volume, string address)
        {
            throw new NotImplementedException();
        }

        public void SetApiAccess(string privateKey, string publicKey, string passphrase)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public async Task<WrappedResponse<ICollection<TradeItem>>> GetTradeHistory(ISymbol symbol, int pageNumber, int pageSize, string fromTradeId)
        {
            var relativeUrl = "TradesHistory";

            var nvc = new NameValueCollection();
            nvc.Add("pair", $"{Exchange.GetCurrencyCode(symbol.BaseCurrencyCode)}{Exchange.GetCurrencyCode(symbol.QuoteCurrencyCode)}");

            return await InternalRequest<KrakenTradeHistory, ICollection<TradeItem>>(true, relativeUrl, HttpMethod.Post, nvc);
        }

        public Task<WrappedResponse<ICollection<Deposit>>> GetDeposits(CurrencyCodeEnum currencyCode, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<WrappedResponse<Deposit>> GetDeposit(CurrencyCodeEnum currencyCode, string transactionId)
        {
            throw new NotImplementedException();
        }

        public async Task<WrappedResponse<ICollection<Mar