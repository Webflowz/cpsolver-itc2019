using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CryptoCurrency.Repository.Edm.Historian
{
    public class HistorianDesignTimeDbContextFactory : IDesignTimeDbContextFactory<HistorianDbContext>
    {
        private IOptions<DbContextConfigurationOptions> Configuration { get; set; }

        private ILoggerFactory LoggerFactory { ge