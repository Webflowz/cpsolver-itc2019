using System.Collections.Generic;

namespace CryptoCurrency.Core.Interval
{
    public interface IIntervalFactory
    {
        IIntervalGroup GetGroup(IntervalGroupEnum group);

        ICollec