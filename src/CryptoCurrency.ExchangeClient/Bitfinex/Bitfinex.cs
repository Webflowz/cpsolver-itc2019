﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Exchange.Model;
using CryptoCurrency.Core.Extensions;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.ExchangeClient.Bitfinex
{
    public class Bitfinex : IExchange
    {
        private ICurrencyFactory CurrencyFactory { get; set; }

        private ISymbolFactory SymbolFactory { get; set; }

        public Bitfinex(ICurrencyFactory currencyFactory, ISymbolFactory symbolFactory)
        {
            CurrencyFactory = currencyFactory;
            SymbolFactory = symbolFactory;
        }

        public int Id => 6;

        public ExchangeEnum Name => ExchangeEnum.Bitfinex;

        public ICollection<ExchangeCurrencyConverter> CurrencyConverter => new List<ExchangeCurrencyConverter>();

        public ICollection<SymbolCodeEnum> Symbol
        {
            get
            {
                return new List<SymbolCodeEnum>
                {
                    SymbolCodeEnum.BTCUSD,
                    SymbolCodeEnum.ETHUSD,
                    SymbolCodeEnum.LTCUSD,
      