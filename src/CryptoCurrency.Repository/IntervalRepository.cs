using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Interval;
using CryptoCurrency.Repository.Edm.Historian;

namespace CryptoCurrency.Repository
{
    public class IntervalRepository : IIntervalRepository
    {
        private IIntervalFactory IntervalFactory { get; set; }

        private IDesignTimeDbContextFactory<HistorianDbContext> ContextFactory { get; set; }

        public IntervalRepository(IIntervalFactory intervalFactory, IDesignTimeDbContextFactory<HistorianDbContext> contextFactory)
        {
            IntervalFactory = intervalFactory;

            ContextFactory = contextFactory;
        }

        public async Task Add(IntervalKey intervalKey)
        {
            using (var context = ContextFactory.CreateDbContext(null))
            {
                if (await context.IntervalKey.FindAsync(intervalKey.Key) == null)
                {
                    await context.IntervalKey.AddAsync(new IntervalKeyEntity
                    {
                        IntervalKey = intervalKey.Key,
                        IntervalGroupId = (int)intervalKey.IntervalGroup,
                        Label = intervalKey.Label
                    });

                    await context.SaveChangesAsync();

                    var from = new DateTime(2008, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    var to = new DateTime(DateTime.Now.Year + 2, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                    var cursor = from;

                    while (cursor < to)
                    {
                        var intervals = IntervalFactory.GenerateIntervals(intervalKey, new Epoch(cursor), new Epoch(cursor.AddYears(1)));

                        await AddInterval(intervals);

                        cursor = cursor.AddYears(1);
                    }
                }
            }
        }

        public async Task AddInterval(ICollection<Interval> interval)
        {
            using 