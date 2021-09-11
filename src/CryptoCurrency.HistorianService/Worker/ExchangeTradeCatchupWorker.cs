using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Historian;
using CryptoCurrency.Core.Historian.Model;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.StorageTransaction;

using CryptoCurrency.HistorianService.Extension;
using CryptoCurrency.HistorianService.Provider;

using CryptoCurrency.Repository.Edm.Historian;
using CryptoCurrency.Core.Exchange.Model;

namespace CryptoCurrency.HistorianService.Worker
{
    public class ExchangeTradeCatchupWorker : IExchangeTradeCatchupWorker
    {
        private ILoggerFactory LoggerFactory { get; set; }
        
        private ISymbolFactory SymbolFactory { get; set; }

        private IStorageTransactionFactory<HistorianDbContext> StorageTransactionFactory { get; set; }

        private IExchangeTradeProvider ExchangeTradeProvider { get; set; }

        private IHistorianRepository HistorianRepository { get; set; }

        private IExchangeWorker ExchangeWorker { get; set; }

        private ILogger Logger { get; set; }

        private IExchange Exchange { get { return ExchangeWorker.Exchange; } }

        private IExchangeHttpClient HttpClient { get; set; }

        public ExchangeTradeCatchupWorker(
            ILoggerFactory loggerFactory,
            ISymbolFactory symbolFactory,
            IStorageTransactionFactory<HistorianDbContext> storageTransactionFactory,
            IExchangeTradeProvider exchangeTradeProvider,
            IHistorianRepository historianRepository)
        {
            LoggerFactory = loggerFactory;
            SymbolFactory = symbolFactory;
            StorageT