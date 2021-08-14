using System;

using Microsoft.Extensions.Logging;

using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.HistorianService.Extension
{
    public static class LoggerExtension
    {
        public static IDisposable BeginEx