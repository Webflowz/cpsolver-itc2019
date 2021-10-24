using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Logging;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Historian.Model;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.Repository.Logging
{
    public class HistorianLogger : ILogger
    {
        private HistorianLoggerProvider LoggerProvider { get; set; }

        private string CategoryName { get; set; }

        private Dictionary<string, object> State { get; set; }
        
        public HistorianLogger(HistorianLoggerProvider loggerProvider, string categoryName)
        {
     