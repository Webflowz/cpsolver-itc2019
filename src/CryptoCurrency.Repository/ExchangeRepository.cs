﻿
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
            using (var context = ContextFactory.CreateDbContext(null))
            {
                var entity = new ExchangeEntity
                {
                    Id = (int)exchange.Name,
                    Name = exchange.Name.ToString()
                };

                if(await context.Exchange.FindAsync(entity.Id) == null)
                {
                    await context.Exchange.AddAsync(entity);

                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task AddSymbol(ExchangeEnum exchange