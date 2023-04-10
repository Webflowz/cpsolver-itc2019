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
            currency.Add(new Ripple());
            currency.Add(new Aud());
            currency.Add(new Eur());
            currency.Add(new Usd());
            currency.Add(new Iota());
            currency.Add(new Neo());
            currency.Add(new Dash());
            currency.Add(new Tether());
            currency.Add(new StellarLumens());
            currency.Add(new BinanceCoin());
            currency.Add(new Monero());
            currency.Add(new EOS());
            currency.Add(new Zcash());
            currency.Add(new TRON());
            currency.Add(new Qtum());
            currency.Add(new Verge());
            currency.Add(new OmiseGo());
            currency.Add(new NEM());
            currency.Add(new Cardano());
            currency.Add(new Lisk());
            currency.Add(new ICON());
            currency.Add(new Stratis());
            currency.Add(new BitShares());
            currency.Add(new Siacoin());
            currency.Add(new AdEx());
            currency.Add(new Waves());
            currency.Add(new Golem());
            currency.Add(new Status());
            currency.Add(new DigixDAO());
           