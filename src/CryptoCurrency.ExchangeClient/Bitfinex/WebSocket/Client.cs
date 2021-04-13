
﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.Exchange.Model;

using CloseEventArgs = CryptoCurrency.Core.Exchange.CloseEventArgs;
using CryptoCurrency.Core.Market;

namespace CryptoCurrency.ExchangeClient.Bitfinex.WebSocket
{
    public class Client : IExchangeWebSocketClient
    {
        private Bitfinex Exchange { get; set; }

        private ISymbolFactory SymbolFactory { get; set; }

        private WebSocketSharp.WebSocket WebSocketClient { get; set; }

        private Dictionary<long, SubscriptionEventResponse> Channels { get; set; }

        public Client(Bitfinex ex, ISymbolFactory symbolFactory)
        {
            Exchange = ex;

            SymbolFactory = symbolFactory;
        }

        public string Url => "wss://api.bitfinex.com/ws/2";

        public bool IsSubscribeModel => true;

        public event EventHandler OnOpen;

        public event EventHandler<CloseEventArgs> OnClose;

        public event EventHandler<TradesReceivedEventArgs> OnTradesReceived;

        public event EventHandler<TickerReceivedEventArgs> OnTickerReceived;

        public void SetApiAccess(string privateKey, string publicKey, string passphrase)
        {

        }

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