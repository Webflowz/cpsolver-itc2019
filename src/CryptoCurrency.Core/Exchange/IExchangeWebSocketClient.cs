using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.Core.Exchange
{
    public interface IExchangeWebSocketClient
    {
        string Url { get; }

        bool IsSubscribeModel { get; }

 