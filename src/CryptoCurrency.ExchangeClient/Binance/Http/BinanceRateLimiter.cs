using System.Threading.Tasks;

using CryptoCurrency.Core.RateLimiter;

namespace CryptoCurrency.ExchangeClient.Binance.Http
{
    public class BinanceRateLimiter : IRateLimiter
    {
        public int Count { get; set; }

        private int MaxCount { get; set; }

        public BinanceRateLimiter()
        {
