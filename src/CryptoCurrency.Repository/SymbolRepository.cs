using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Repository.Edm.Historian;
using Microsoft.EntityFrameworkCore.Design;

namespace CryptoCurrency.Repository
{
    public class SymbolRepository : ISymbolRepository
    {
        private ILoggerFactory LoggerFactory { get; set; }

        private IDesignTimeDbContextFactory<HistorianDbContext> ContextFactory { get; set; }

        public SymbolRepository(ILoggerFactory loggerFactory, IDesignTimeDbContextFactory<HistorianDbContext> contextFactory)
       