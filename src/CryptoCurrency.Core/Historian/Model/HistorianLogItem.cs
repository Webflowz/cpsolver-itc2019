using System;

using Microsoft.Extensions.Logging;

using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.Core.Historian.Model
{
    public class HistorianLogItem
    {
        public Epoch Epoch { get; set; }

 