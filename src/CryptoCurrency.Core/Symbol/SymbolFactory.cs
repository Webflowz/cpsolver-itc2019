﻿using System;
using System.Collections.Generic;
using System.Linq;

using CryptoCurrency.Core.Currency;

namespace CryptoCurrency.Core.Symbol
{
    public class SymbolFactory : ISymbolFactory
    {
        private ICollection<ISymbol> Symbols { get; set; }

        public SymbolFactory(ICurrencyFactory currencyFactory)
        {
            var currencies = currencyFactory.List();

            Symbols = new List<ISymbol>();

            foreach(var symbolCodeEnum in Enum.GetValues(typeof(SymbolCodeEnum)))
            {
                var symbolCode = symbolCodeEnum.ToString();

                var tradable = !symbolCode.EndsWith("SHORTS") && !symbolCode.EndsWith("LONGS");

                var currencyPair = symbolCode.Replace("SHORTS", "").Replace("LONGS", "");
                
                ICurrency baseCurrency = null;
                ICurrency quoteCurrency = null;

                var baseCandidates = currencies.Where(c => currencyPair.StartsWith(c.Code.ToString()));

                foreach(var baseCandidate in baseCandidates)
                {
                    var len = baseCandidate.Code.ToString().Length;

                    var quoteCurrencyPair = currencyPair.Substring(len, currencyPair.Length - len);

                    var quoteCandidate = currencies.Where(c => c.Code.ToString().Equals(quoteCurrencyPair)).FirstOrDefault();

                    if(quoteCandidate != null)
                    {
                        baseCurrency = baseCandidate;
                        quoteCurrency = quoteCandidate;
                        break;
                    }
                }

                if(baseCurrency != null && quoteCurrency != null)
                {
                    var symbol = new Symbol((SymbolCodeEnum)symbolCodeEnum, baseCurrency.Code, quoteCurrency.Code, tradable);

                    Symbo