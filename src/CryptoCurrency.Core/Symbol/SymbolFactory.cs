using System;
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

                var tradable = !symbolCode.EndsWith("SHORTS") && !symbolCode.EndsWit