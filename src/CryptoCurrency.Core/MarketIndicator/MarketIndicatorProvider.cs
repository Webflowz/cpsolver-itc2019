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

            var aggValues = await MarketRepository.GetTradeAggregates(exchange, symbolCode, intervalKey, fromOffset.From, periodOffset + dataPoints);

            var values = aggValues.GetValues(candleType);

            int outBegIdx, outNbElement;

            var smaValues = new double[dataPoints];

            var retCode = TicTacTec.TA.Library.Core.Sma(0, values.Length - 1, values, period, out outBegIdx, out outNbElement, smaValues);

            var validSmaValues = smaValues.Skip(outNbElement - dataPoints).Take(dataPoints).ToArray();

            var validAggValues = aggValues.Skip(aggValues.Count - dataPoints).Take(dataPoints).ToArray();

            if (retCode == RetCode.Success)
            {
                var dp = new List<MovingAverageDataPoint>();

                for (var i = 0; i < validAggValues.Length; i++)
                {
                    var agg = validAggValues[i];

                    dp.Add(new MovingAverageDataPoint
                    {
                        Epoch = agg.Epoch,
                        Value = validSmaValues[i]
                    });
                }

                return dp;
            }

            throw new Exception("Unable to calculate SMA - " + retCode);
        }

        public async Task<ICollection<MovingAverageDataPoint>> Ema(ExchangeEnum exchange, SymbolCodeEnum symbolCode, IntervalKey intervalKey, Epoch from, int dataPoints, CandleTypeEnum candleType, int period)
        {
            var periodOffset = period - 1;

            var fromOffset = IntervalFactory.GetInterval(intervalKey, from, periodOffset * -1);

            var aggValues = await MarketRepository.GetTradeAggregates(exchange, symbolCode, intervalKey, fromOffset.From, periodOffset + dataPoints);

            var values = aggValues.GetValues(candleType);

            int outBegIdx, outNbElement;

            var smaValues = new double[dataPoints];

            var retCode = TicTacTec.TA.Library.Core.Ema(0, values.Length - 1, values, period, out outBegIdx, out outNbElement, smaValues);

            var validSmaValues = smaValues.Skip(outNbElement - dataPoints).Take(dataPoints).ToArray();

            var validAggValues = aggValues.Skip(aggValues.Count - dataPoints).Take(dataPoints).ToArray();

            if (retCode == RetCode.Success)
            {
                var dp = new List<MovingAverageDataPoint>();

                for (var i = 0; i < validAggValues.Length; i++)
                {
                    var agg = validAggValues[i];

                    dp.Add(new MovingAverageDataPoint
                    {
                        Epoch = agg.Epoch,
                        Value = validSmaValues[i]
                    });
                }

                return dp;
            }

            throw new Exception("Unable to calculate EMA - " + retCode);
        }

        public async Task<ICollection<RsiDataPoint>> Rsi(ExchangeEnum exchange, SymbolCodeEnum symbolCode, IntervalKey intervalKey, Epoch from, int dataPoints, CandleTypeEnum candleType, int rsiPeriod)
        {
            var fromOffset = IntervalFactory.GetInterval(intervalKey, from, rsiPeriod * -1);

            var aggValues = await MarketRepository.GetTradeAggregates(exchange, symbolCode, intervalKey, fromOffset.From, rsiPeriod + dataPoints);

            var values = aggValues.GetValues(candleType);

            int outBegIdx, outNbElement;

            var rsiValues = new double[dataPoints];

            var retCode = TicTacTec.TA.Library.Core.Rsi(0, values.Length - 1, values, rsiPeriod, out outBegIdx, out outNbElement, rsiValues);

            var validRsiValues = rsiValues.Skip(outNbElement - dataPoints).Take(dataPoints).ToArray();
            
            var validAggValues = aggValues.Skip(aggValues.Count - dataPoints).Take(dataPoints).ToArray();

            if (retCode == RetCode.Success)
            {
                var dp = new List<RsiDataPoint>();

                for (var i = 0; i < validAggValues.Length; i++)
                {
                    var agg = 