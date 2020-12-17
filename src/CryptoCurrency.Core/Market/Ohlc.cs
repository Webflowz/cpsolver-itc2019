using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.Core.Market
{
    public class Ohlc
    {
        public ExchangeEnum Exchange { get; set; }

        public SymbolCodeEnum Symbol { get; set; }

        public string 