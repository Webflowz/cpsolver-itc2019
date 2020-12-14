using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCurrency.Core.Interval
{
    public class IntervalFactory : IIntervalFactory
    {
        private ICollection<IIntervalGroup> IntervalGroup { get; set; }

        private Dictionary<IntervalGroupEnum, Dictionary<string, IntervalKey>> IntervalKey { get; set; }

        public IntervalFactory(IEnumerable<IIntervalGroup> intervalGroup)
        {
            IntervalGroup = intervalGroup.ToList();

            IntervalKey = new Dictionary<IntervalGroupEnum, Dictionary<string, IntervalKey>>();

            foreach(var group in intervalGroup)
            {
                var keys = new Dictionary<string, IntervalKey>();

                foreach (var duration in group.SupportedDuration)
                {
                    var key = $"{duration}{group.SuffixKey}";

                    keys.Add(key, new IntervalKey()
                    {
                        IntervalGroup = group.IntervalGroup,
                        Key = key,
                        Duration = duration,
                        Label = $"{duration} {group.SuffixLabel}{(duration > 1 ? "s" : "")}"
                    });
                }

                IntervalKey.Add(group.IntervalGroup, keys);
            }
        }

        public IIntervalGroup GetGroup(IntervalGroupEnum group)
        {
            var match = IntervalGroup.Where(g => 