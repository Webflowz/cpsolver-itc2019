
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;

using CryptoCurrency.Repository.Edm.Historian;
using System.Threading.Tasks;

namespace CryptoCurrency.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private ILoggerFactory LoggerFactory { get; set; }

        private IDesignTimeDbContextFactory<HistorianDbContext> ContextFactory { get; set; }

        public ExchangeRepository(ILoggerFactory loggerFactory, IDesignTimeDbContextFactory<HistorianDbContext> contextFactory)
        {
            LoggerFactory = loggerFactory;

            ContextFactory = contextFactory;
        }

        public async Task Add(IExchange exchange)
        {
            using (va