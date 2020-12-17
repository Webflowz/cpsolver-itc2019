using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static TicTacTec.TA.Library.Core;

using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Extensions;
using CryptoCurrency.Core.Interval;
using CryptoCurrency.Core.Market;
using CryptoCurrency.Core.MarketIndicator.Model;
using CryptoCurrency.Core.Symbol;

namespace CryptoCurrency.Core.MarketIndicator
{
    public class MarketIndicatorProvider : IMarketIndicatorProvider
    {
        private IIntervalFactory IntervalFactory { get; set; }

        private IMarketRepository MarketRepository { get; set; }

        public MarketIndicatorProvider(IIntervalFactory intervalFactory, IMarketRepository marketRepository)
        {
            IntervalFactory = intervalFactory;

            MarketRepository = marketRepository;
        }

        public async Task<ICollection<MovingAverageDataPoint>> Sma(ExchangeEnum exchange, SymbolCodeEnum symbolCode, IntervalKey intervalKey, Epoch from, int dataPoints, CandleTypeEnum candleType, int period)
        {
            var periodOffset = period - 1;

            var fromOffset = IntervalFactory.GetInterval(intervalKey, from, periodOffset * -1);

            var aggValues = await MarketRepository.GetTradeAggregates(exchange, symbolCode, intervalKey, fromOffset.From, per