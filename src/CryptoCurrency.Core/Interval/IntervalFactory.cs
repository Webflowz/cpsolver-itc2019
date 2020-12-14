﻿using System;
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
            var match = IntervalGroup.Where(g => g.IntervalGroup == group).FirstOrDefault();

            if (match == null)
                throw new Exception($"Unable to find group '{group}'");

            return match;
        }

        public ICollection<IIntervalGroup> ListGroups()
        {
            return IntervalGroup;
        }

        public ICollection<IntervalKey> ListIntervalKeys(IntervalGroupEnum intervalGroup)
        {
            return IntervalKey[intervalGroup].Values;
        }

        public IntervalKey GetIntervalKey(string intervalKey)
        {
            foreach(var group in IntervalKey)
            {
                if (group.Value.ContainsKey(intervalKey))
                    return group.Value[intervalKey];
            }

            throw new ArgumentException($"Unable to find '{intervalKey}'");
        }

        public ICollection<Interval> GenerateIntervals(IntervalKey intervalKey, Epoch from, Epoch to)
        {
            var group = GetGroup(intervalKey.IntervalGroup);

            var intervals = new List<Interval>();

            var lastInterval = group.GetInterval(intervalKey, to);

            var intervalCursor = group.GetInterval(interv