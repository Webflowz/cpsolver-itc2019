using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.Currency;

namespace CryptoCurrency.ExchangeClient.Tests
{
    public class ExchangeWebSocketClientTests
    {
        private ISymbolFactory SymbolFactory { get; set; }

        private IExchange Exchange { get; set; }

        public ExchangeWebSocketClientTests(IExchange exchange)
        {
            Exchange = exchan