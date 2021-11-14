using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Interval;
using CryptoCurrency.Core.Market;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.StorageTransaction;

using CryptoCurrency.Repository.Edm.Historian;
using System;

namespace CryptoCurrency.Repository
{
    public class MarketRepository : IMarketRepository
    {
        private ILogger Logger { get; set; }

        private IIntervalFactory IntervalFactory { get; set; }

        private IDesignTimeDbContextFactory<HistorianDbContext> ContextFactory { get; set; }

        private IStorageTransactionFactory<HistorianDbContext> StorageTransactionFactory { get; set; }

        public MarketRepository(
            ILoggerFactory loggerFactory, 
            IIntervalFactory intervalFactory, 
            IDesignTimeDbContextFactory<HistorianDbContext> contextFactory,
            IStorageTransactionFactory<HistorianDbContext> storageTransactionFactory)
        {
            IntervalFactory = intervalFactory;
            ContextFactory = contextFactory;
            StorageTransactionFactory = storageTransactionFactory;

            Logger = loggerFactory.CreateLogger<MarketRepository>();
        }

        public async Task<MarketTick> GetTick(ExchangeEnum exchange, SymbolCodeEnum symbolCode, Epoch at)
        {
            using (var context = ContextFactory.CreateDbContext(null))
            {
                var minAt = at.AddSeconds((int)TimeSpan.FromDays(-1).TotalSeconds);

                var buyTradeQuery = 
                    from 
                        t 
                    in 
                        context.ExchangeTrade
                    where 
                        t.ExchangeId == (int)exchange && 
                        t.SymbolId == (int)symbolCode && 
                        t.Timestamp >= minAt.TimestampMilliseconds &&
                        t.Timestamp <= at.TimestampMilliseconds && 
                        t.OrderSideId == (int)OrderSideEnum.Buy
                    orderby
                        t.ExchangeId,
                        t.SymbolId,
                        t.Timestamp descending
                    select 
                        t;

                var buyTrade = await buyTradeQuery.FirstOrDefaultAsync();

                var sellTradeQuery = 
                    from 
                        t 
                    in 
                        context.ExchangeTrade
                    where 
                        t.ExchangeId == (int)exchange && 
                        t.SymbolId == (int)symbolCode &&
                        t.Timestamp >= minAt.TimestampMilliseconds &&
                        t.Timestamp <= at.TimestampMilliseconds && 
                        t.OrderSideId == (int)OrderSideEnum.Sell
                    orderby
                        t.ExchangeId,
                        t.SymbolId,
                        t.Timestamp descending
                    select 
                        t;

                var sellTrade = await sellTradeQuery.FirstOrDefaultAsync();

                ExchangeTradeEntity lastTrade = null;

                if (buyTrade != null && sellTrade != null)
                {
                    lastTrade = buyTrade.Timestamp <= sellTrade.Timestamp ? buyTrade : sellTrade;
                }
                else
                {
                    var lastTradeQuery = 
                        from 
                            t 
                        in 
                            context.ExchangeTrade
                        where
                            t.ExchangeId == (int)exchange && 
                            t.SymbolId == (int)symbolCode &&
                            t.Timestamp >= minAt.TimestampMilliseconds &&
                            t.Timestamp <= at.TimestampMilliseconds
                        orderby
                            t.ExchangeId,
                            t.SymbolId,
                            t.Timestamp descending
                        select 
                            t;

                    lastTrade = await lastTradeQuery.FirstOrDefaultAsync();
                }

                if (lastTrade == null)
                    return null;

                return new MarketTick
                {
                    Exchange = exchange,
                    SymbolCode = symbolCode,
                    Epoch = at,
                    BuyPrice = buyTrade != null ? buyTrade.Price : lastTrade.Price,
                    SellPrice = sellTrade != null ? sellTrade.Price : lastTrade.Price,
                    LastPrice = lastTrade.Price
                };
            }
        }

        public async Task<MarketTickAverage> GetTickAverage(ICollection<ExchangeEnum> exchanges, SymbolCodeEnum symbolCode, Epoch at)
        {
            var ticks = new Dictionary<ExchangeEnum, MarketTick>();

            foreach(var exchange in exchanges)
            {
                var tick = await GetTick(exchange, symbolCode, at);

                if(tick != null)
                    ticks.Add(exchange, tick);
            }

            if (ticks.Count == 0)
                return null;

            return new MarketTickAverage
            {
                Epoch = at,
                SymbolCode = symbolCode,
                BuyPrice = ticks.Values.Average(t => t.BuyPrice),
                SellPrice = ticks.Values.Average(t => t.SellPrice),
                LastPrice = ticks.Values.Average(t => t.LastPrice),
                Exchanges = ticks.Keys.ToList()
            };
        }

        public async Task<PagedCollection<MarketTrade>> GetTrades(ExchangeEnum exchange, SymbolCodeEnum symbolCode, Epoch from, Epoch to, int? pageSize, int? pageNumber)
        {
            using (var context = ContextFactory.CreateDbContext(null))
            {
                var query =
                    from
                        t
                    in
                        context.ExchangeTrade
                    where
                        t.ExchangeId == (int)exchange &&
                        t.SymbolId == (int)symbolCode &&
                        t.Timestamp >= @from.TimestampMilliseconds &&
                        t.Timestamp <= to.TimestampMilliseconds
                    select new
                    {
                        Exchange = exchange,
                        SymbolCode = symbolCode,
                        Timestamp = t.Timestamp,
                        TradeId = t.TradeId,
                        Side = t.OrderSideId.HasValue ? (OrderSideEnum)t.OrderSideId : (OrderSideEnum?)null,
                        Price = t.Price,
                        Volume = t.Volume
                    };

                var totalCount = 0;

                if (pageSize.HasValue)
                {
                    totalCount = await query.CountAsync();

                    query = query.Skip(pageNumber.GetValueOrDefault(0) * pageSize.Value).Take(pageSize.Value);
                }

                var trades = await query.ToListAsync();

                return new PagedCollection<MarketTrade>()
                {
                    PageNumber = pageNumber.GetValueOrDefault(0),
                    PageSize = pageSize.GetValueOrDefault(0),
                    ItemCount = pageSize.HasValue ? totalCount : trades.Count,
                    Items = trades.Select(t => new MarketTrade
                    {
                        Exchange = t.Exchange,
                        SymbolCode = t.SymbolCode,
                        Epoch = Epoch.FromMilliseconds(t.Timestamp),
                        TradeId = t.TradeId,
                        Side = t.Side,
                        Price = t.Price,
                        Volume = t.Volume
                    }).ToList()
                };
            }
        }

        public async Task SaveTrades(IStorageTransaction transaction, ICollection<MarketTrade> trades)
        {
            var context = (HistorianDbContext)transaction.GetContext();

            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.Transaction = context.Database.CurrentTransaction.GetDbTransaction();
                
                var sql = @"insert ignore