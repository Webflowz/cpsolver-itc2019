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
            Exchange = exchange;

            SymbolFactory = CommonMock.GetSymbolFactory();
        }

        [TestMethod]
        public void CanConnect()
        {
            var resetEvent = new ManualResetEvent(false);

            var webSocketClient = Exchange.GetWebSocketClient();

            if (webSocketClient == null)
            {
                Assert.Fail($"Web Socket client not available for {Exchange.Name}");

                return;
            }

            var retry = 0;

            webSocketClient.OnOpen += delegate (object sender, EventArgs e)
            {
                resetEv