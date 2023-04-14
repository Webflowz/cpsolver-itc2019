﻿using System;
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
            currency.Add(new Augur());
            currency.Add(new Zrx());
            currency.Add(new IOStoken());
            currency.Add(new Nano());
            currency.Add(new BasicAttentionToken());
            currency.Add(new Monaco());
            currency.Add(new Steem());
            currency.Add(new Civic());
            currency.Add(new Aelf());
            currency.Add(new PowerLedger());
            currency.Add(new Poet());
            currency.Add(new Cindicator());
            currency.Add(new Storj());
            currency.Add(new Decentraland());
            currency.Add(new Rcoin());
            currency.Add(new FunFair());
            currency.Add(new Syscoin());
            currency.Add(new Ark());
            currency.Add(new Enigma());
            currency.Add(new Walton());
            currency.Add(new Ontology());
            currency.Add(new NAVCoin());
            currency.Add(new BitcoinDiamond());
            currency.Add(new Tierion());
            currency.Add(new TimeNewBank());
            currency.Add(new district0x());
            currency.Add(new Bancor());
            currency.Add(new Gas());
            currency.Add(new BlockMason());
            currency.Add(new NucleusVision());
            currency.Add(new Decred());
            currency.Add(new Ardor());
            currency.Add(new Neblio());
            currency.Add(new Viberate());
            currency.Add(new RequestNetwork());
            currency.Add(new Gifto());
            currency.Add(new AppCoins());
            currency.Add(new Viacoin());
            currency.Add(new Quantstamp());
            currency.Add(new ETHLend());
            currency.Add(new Komodo());
            currency.Add(new SingularDTV());
            currency.Add(new ChainLink());
            currency.Add(new ZCoin());
            currency.Add(new Lunyr());
            currency.Add(new Zilliqa());
            currency.Add(new PIVX());
            currency.Add(new KyberNetwork());
            currency.Add(new AirSwap());
            currency.Add(new CyberMiles());
            currency.Add(new SimpleToken());
            currency.Add(new Nebulas());
            currency.Add(new VIBE());
            currency.Add(new Aion());
            currency.Add(new Storm());
            currency.Add(new Bluzelle());
            currency.Add(new SONM());
            currency.Add(new iExecRLC());
            currency.Add(new EnjinCoin());
            currency.Add(new Eidoo());
            currency.Add(new GenesisVision());
            currency.Add(new Loopring());
            currency.Add(new CoinDash());
            currency.Add(new INSEcosystem());
            currency.Add(new Everex());
            currency.Add(new Groestlcoin());
            currency.Add(new RaidenNetworkToken());
            currency.Add(new ThetaToken());
            currency.Add(new Aeternity());
            currency.Add(new Nuls());
            currency.Add(new Populous());
            currency.Add(new Nexus());
            currency.Add(new Ambrosus());
            currency.Add(new Aeron());
            currency.Add(new ZenCash());
            currency.Add(new WaBi());
            currency.Add(new Etherparty());
            currency.Add(new OpenAnx());
            currency.Add(new POANetwork());
            currency.Add(new Bread());
            currency.Add(new Agrel