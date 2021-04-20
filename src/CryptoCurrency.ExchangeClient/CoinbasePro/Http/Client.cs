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
using CryptoCurrency.Core.OrderType;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.RateLimiter;
using CryptoCurrency.Core.Symbol;

using CryptoCurrency.ExchangeClient.CoinbasePro.Model;

namespace CryptoCurrency.ExchangeClient.CoinbasePro.Http
{
    public class Client : IExchangeHttpClient
    {
        private CoinbasePro Exchange { get; set; }

        private ICurrencyFactory CurrencyFactory { get; set; }

        private ISymbolFactory SymbolFactory { get; set; }

        public IRateLimiter RateLimiter { get; set; }

        public Client(CoinbasePro exchange, ICurrencyFactory currencyFactory, ISymbolFactory symbolFactory)
        {
            Exchange = exchange;
            CurrencyFactory = currencyFactory;
            SymbolFactory = symbolFactory;

            RateLimiter = new CoinbaseProRateLimiter();
        }

        public string ApiUrl => "https://api.pro.coinbase.com";

        public bool MultiTickSupported => false;

        public string InitialTradeFilter => "100";
        
        public async Task<WrappedResponse<CancelOrder>> CancelOrder(ISymbol symbol, string[] orderIds)
        {
            var relativeUrl = $"/orders/{orderIds.First()}";

            return await InternalRequest<CoinbaseProCancelOrder, CancelOrder>(true, relativeUrl, HttpMethod.Delete, null);
        }

        public async Task<WrappedResponse<CreateOrder>> CreateOrder(ISymbol symbol, OrderTypeEnum orderType, OrderSideEnum orderSide, decimal price, decimal volume)
        {
            var relativeUrl = "/orders";

            var request = new CoinbaseProCreateOrderRequest
            {
                Size = volume,
                Price = price,
                Side = orderSide == OrderSideEnum.Buy ? "buy" : "sell",
                ProductId = Exchange.EncodeProductId(symbol),
                Type = orderType == OrderTypeEnum.Market ? "market" : "limit"
            };

            return await InternalRequest<CoinbaseProCreateOrderResponse, CreateOrder>(true, relativeUrl, HttpMethod.Post, request);
        }

        public async Task<WrappedResponse<ICollection<AccountBalance>>> GetBalance()
        {
            var relativeUrl = "/accounts";

            return await InternalRequest<ICollection<CoinbaseProAccount>, ICollection<AccountBalance>>(true, relativeUrl, HttpMethod.Get, null);
        }

        public Task<WrappedResponse<Deposit>> GetDeposit(CurrencyCodeEnum currencyCode, string transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<Wrappe