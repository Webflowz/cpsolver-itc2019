using CryptoCurrency.Core.Currency;

namespace CryptoCurrency.Core.Symbol
{
    public class Symbol : ISymbol
    {
        public SymbolCodeEnum Code { get; set; }

        public CurrencyCodeEnum BaseCurrencyCode { get; set; }

        public Curre