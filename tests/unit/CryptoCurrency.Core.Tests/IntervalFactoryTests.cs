using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using CryptoCurrency.Core.Interval;
using CryptoCurrency.Core.Interval.Group;

namespace CryptoCurrency.Core.Tests
{
    [TestFixture]
    public class IntervalFactoryTests
    {
        private IIntervalFactory IntervalFactory { get; set; }
        
        [SetUp]
        protected void SetUp()
        {
            var groups = new List<IIntervalGroup>();
            groups.Add(new Minute());
            groups.Add(new Hour());
            groups.Add(new Day());
            groups.Add(new Week());
            groups.Add(new Month());

            IntervalFactory = new IntervalFactory(groups);
        }

        [Test]
        public void GeneratesExpectedIntervalCountFor1d()
        {
            var from = new Epoch(new DateTime(2018, 1, 1, 0, 30, 0, Da