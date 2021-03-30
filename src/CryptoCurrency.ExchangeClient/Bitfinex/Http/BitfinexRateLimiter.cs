
﻿using System.Threading.Tasks;

using CryptoCurrency.Core.RateLimiter;

namespace CryptoCurrency.ExchangeClient.Bitfinex.Http
{
    public class BitfinexRateLimiter : IRateLimiter
    {
        public int Count { get; set; }

        private int MaxCount { get; set; }

        public BitfinexRateLimiter()
        {
            Count = 0;

            MaxCount = 3;

            FillBucket();
        }

        private void FillBucket() => Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(4000);

                if (Count > 0)
                    Count--;
            }
        });

        public async Task Wait()
        {
            while (true)
            {
                if (Count + 1 < MaxCount)
                {
                    Count++;

                    return;
                }

                await Task.Delay(5);
            }
        }
    }
}