﻿using System.Collections.Generic;
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

            foreach(var e