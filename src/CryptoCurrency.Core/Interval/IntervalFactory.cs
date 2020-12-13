using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCurrency.Core.Interval
{
    public class IntervalFactory : IIntervalFactory
    {
        private ICollection<IIntervalGroup> IntervalGroup { get; set; }

        private Dictionary<IntervalGroupEnum, Dictionary<string, IntervalK