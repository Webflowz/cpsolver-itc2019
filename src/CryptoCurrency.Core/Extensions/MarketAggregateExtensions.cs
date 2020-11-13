using System;
using System.Collections.Generic;
using System.Linq;

using CryptoCurrency.Core.Market;

namespace CryptoCurrency.Core.Extensions
{
    public static class MarketAggregateExtensions
    {
        public static double[] GetValues(this ICollection<MarketAggregate> aggregates, CandleTypeEnum candleType)
        {
            switch(candleType)
            {
                case CandleTypeEnum.Open:
                    return aggregates.Sele