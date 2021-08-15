using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Historian;
using CryptoCurrency.ExchangeClient;
using CryptoCurrency.HistorianService.Provider;
using CryptoCurrency.Repository;
using CryptoCurrency.Repository.Extension;
using CryptoCurrency.Repository.Logging;

namespace CryptoCurrency.HistorianService
{
    using Worker;

    class Program
    {
        private static ManualResetEvent ResetEvent = new ManualResetEvent(false);

        static async Task Main(string[] args)
        