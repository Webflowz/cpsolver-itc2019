﻿using System.Threading.Tasks;

using NUnit.Framework;

using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.ExchangeClient.Tests
{
    [TestFixture]
    public class KrakenWebSocketClientTests
    {
        private ICurrencyFactory CurrencyFactory { get; set; }

        private ISymbolFactory SymbolFactory { get; set; }

        private IExchange Exchange { get; set; }

        private ExchangeWebSocketClientTests WebSocketTest { get; set; }

        [SetUp]
        protected async Task SetUp()
        {
            CurrencyFactory = CommonMock.GetCurrencyFactory();
            SymbolFactory = CommonMock.GetSymbolFactory();

            Exchange = new Kraken.Kraken(CurrencyFactory, S