using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Historian;
using CryptoCurrency.Core.Historian.Model;
using CryptoCurrency.Core.Exchange;
using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.StorageTransaction;

using CryptoCurrency.HistorianService.Extension;
using CryptoCurrency.HistorianService.Provider;

using CryptoCurrency.Repository.Edm.Historian;
using CryptoCurrency.Core.Exchange.Model;

namespace CryptoCurrency.HistorianService.Worker
{
    public class Exchang