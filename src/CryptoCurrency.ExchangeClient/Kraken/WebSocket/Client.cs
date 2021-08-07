
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WebSocketSharp;

using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.Extensions;
using CryptoCurrency.Core.Exchange.Model;
using CryptoCurrency.Core.Market;
using CryptoCurrency.Core;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.Currency;

using CloseEventArgs = CryptoCurrency.Core.Exchange.CloseEventArgs;

namespace CryptoCurrency.ExchangeClient.Kraken.WebSocket
{
    public class Client : IExchangeWebSocketClient
    {
        private Kraken Exchange { get; set; }
        private ICurrencyFactory CurrencyFactory { get; set; }
        private ISymbolFactory SymbolFactory { get; set; }

        private WebSocketSharp.WebSocket WebSocketClient { get; set; }

        private Dictionary<long, SubscriptionEventResponse> Channels { get; set; }

        public Client(Kraken ex, ICurrencyFactory currencyFactory, ISymbolFactory symbolFactory)
        {
            Exchange = ex;
            CurrencyFactory = currencyFactory;
            SymbolFactory = symbolFactory;
        }

        public string Url => "wss://ws.kraken.com";

        public bool IsSubscribeModel => true;

        public event EventHandler OnOpen;
        public event EventHandler<CloseEventArgs> OnClose;
        public event EventHandler<TradesReceivedEventArgs> OnTradesReceived;
        public event EventHandler<TickerReceivedEventArgs> OnTickerReceived;

        public Task Begin() => Task.Run(() =>
        {
            if (WebSocketClient != null)
                throw new Exception("WebSocket already in use");

            Channels = new Dictionary<long, SubscriptionEventResponse>();

            WebSocketClient = new WebSocketSharp.WebSocket(Url);

            WebSocketClient.OnOpen += OnOpen;

            WebSocketClient.OnMessage += OnMessage;

            WebSocketClient.OnClose += delegate (object sender, WebSocketSharp.CloseEventArgs e)
            {
                Channels = new Dictionary<long, SubscriptionEventResponse>();

                OnClose?.Invoke(sender, new CloseEventArgs { });
            };

            Connect();
        });

        public void Connect()
        {