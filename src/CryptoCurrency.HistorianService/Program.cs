﻿using System;
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
        {
            // Get configuration
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

            var appConfig = builder.Build();

            var historianConnectionString = appConfig.GetConnectionString("Historian");

            var serviceProvider = new ServiceCollection()
                .AddLogging(opt =>
                {
                    opt.AddConfiguration(appConfig.GetSection("Logging"));
                    opt.AddConsole(options => options.IncludeScopes = true);
                })
                .AddFactories()
                .AddExchangeFactory()
                .AddRepositories(historianConnectionString, historianConnectionString)
                .AddSingleton<IRepositoryBootstrapper, RepositoryBootstrapper>()
                .AddScoped<IExchangeTradeProvider, ExchangeTradeProvider>()
                .AddTransient<IExchangeWorker, ExchangeWorker>()
                .AddTransient<IExchangeTradeWorker, ExchangeTradeWorker>()
                .AddTransient<IExchangeTradeStatWorker, ExchangeTradeStatWorker>()
                .AddTransient<IExchangeTradeAggregateWorker, ExchangeTradeAggregateWorker>()
                .AddTransient<IExchangeTradeStatAggregateWorker, ExchangeTradeStatAggregateWorker>()
                .AddTransient<IExchangeTradeCatchupWorker, ExchangeTradeCatchupWorker>()
                .BuildServiceProvider();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            
            var historianRepository = serviceProvider.GetService<IHistorianRepository>();

            loggerFactory.AddProvider(new HistorianLoggerProvider(historianRepository));
            
            var logger = loggerFactory.CreateLogger("Historian");

            logger.LogInformation("Starting service");
                       
            logger.LogInformation($"Connection String: {historianConnectionString}");
            
            // Warm up the EF core db contexts . . .
            var dbContextWarmUpSuccess = serviceProvider.WarmUpDbContext(logger);

            if (dbContextWarmUpSuccess)
            {
                var bootstrapper = serviceProvider.GetService<IRepositoryBootstrapper>();

            