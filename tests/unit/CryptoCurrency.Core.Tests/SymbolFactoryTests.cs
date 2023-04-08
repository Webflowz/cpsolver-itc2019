using System;
using System.Collections.Generic;


using NUnit.Framework;

using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.Currency;
using System.Linq;

namespace CryptoCurrency.Core.Tests
{
    [TestFixture]
    public class SymbolFactoryTests
    {
        private ICurrencyFactory CurrencyFactory { get; set; }

        private ISymbolFactory SymbolFactory { get; set; }

        [SetUp]
        protected void SetUp()
        {
            var currency = new List<ICurrency>();

            currency.Add(new Bitcoin());
            currency.Add(new Litecoin());
            currency.Add(new Ethereum());
            currency.Add(new EthereumClassic());
      