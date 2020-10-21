using System.Collections.Generic;
using System.Threading.Tasks;

using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.Exchange.Model;
using CryptoCurrency.Core.Market;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.OrderType;
using CryptoCurrency.Core.RateLimiter;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.Core.Exchange
{
    public interface IExchangeHttpClient
    {
        IRateLimiter RateLimiter { get; set; }

        string ApiUrl { get; }

        bool MultiTickSupported { get; }
        
        string Ini