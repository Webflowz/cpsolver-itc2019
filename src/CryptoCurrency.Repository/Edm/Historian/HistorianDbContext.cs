using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoCurrency.Repository.Edm.Historian
{
    public class HistorianDbContext : DbContext
    {
        private ILoggerFactory LoggerFactory { get; set; }
        
        public DbSet<CurrencyEntity> Currency { get; set; }

        public DbSet<SymbolEntity> Symbol { get; set; }

        public DbSet<ExchangeEntity> Exchange { get; set; }
        
        public DbSet<ExchangeSymbolEntity> ExchangeSymbol { get; set; }
        
        public DbSet<ExchangeTradeEntity> ExchangeTrade { get; set; }

        public DbSet<ExchangeTradeStatEntity> ExchangeTradeStat { get; set; }

        public DbSet<ExchangeTradeAggregateEntity> ExchangeTradeAggregate { get; set; }

        public DbSet<IntervalKeyEntity> IntervalKey { get; set; }

        public DbSet<IntervalEntity> Interval { get; set; }

        public DbSet<HistorianExchangeSymbolEntity> HistorianExchangeSymbol { get; set; }

        public DbSet<HistorianTradeCatchupEntity> HistorianTradeCatchup { get; set; }

        public DbSet<HistorianLogEntity> HistorianLog { get; set; }

        public DbSet<OrderSideEntity> OrderSide { get; set; }

        public HistorianDbContext(DbContextOptions<Hist