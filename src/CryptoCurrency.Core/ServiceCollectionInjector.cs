
﻿using Microsoft.Extensions.DependencyInjection;

using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.Interval;
using CryptoCurrency.Core.Interval.Group;
using CryptoCurrency.Core.Symbol;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.Exchange;

namespace CryptoCurrency.Core
{
    public static class ServiceCollectionInjector
    {
        public static IServiceCollection AddFactories(this IServiceCollection serviceCollection)
        {
            // Interval factory
            serviceCollection
                .AddSingleton<IIntervalGroup, Minute>()
                .AddSingleton<IIntervalGroup, Hour>()
                .AddSingleton<IIntervalGroup, Day>()
                .AddSingleton<IIntervalGroup, Week>()
                .AddSingleton<IIntervalGroup, Month>()
                .AddSingleton<IIntervalFactory, IntervalFactory>();

            // Currency factory
            serviceCollection
                .AddSingleton<ICurrency, Bitcoin>()
                .AddSingleton<ICurrency, Litecoin>()
                .AddSingleton<ICurrency, Ethereum>()
                .AddSingleton<ICurrency, EthereumClassic>()
                .AddSingleton<ICurrency, Ripple>()
                .AddSingleton<ICurrency, Aud>()
                .AddSingleton<ICurrency, Eur>()
                .AddSingleton<ICurrency, Usd>()
                .AddSingleton<ICurrency, Iota>()
                .AddSingleton<ICurrency, Neo>()
                .AddSingleton<ICurrency, Dash>()
                .AddSingleton<ICurrency, Tether>()
                .AddSingleton<ICurrency, StellarLumens>()
                .AddSingleton<ICurrency, BinanceCoin>()
                .AddSingleton<ICurrency, Monero>()
                .AddSingleton<ICurrency, EOS>()
                .AddSingleton<ICurrency, Zcash>()
                .AddSingleton<ICurrency, TRON>()
                .AddSingleton<ICurrency, Qtum>()
                .AddSingleton<ICurrency, Verge>()
                .AddSingleton<ICurrency, OmiseGo>()
                .AddSingleton<ICurrency, NEM>()
                .AddSingleton<ICurrency, Cardano>()
                .AddSingleton<ICurrency, Lisk>()
                .AddSingleton<ICurrency, ICON>()
                .AddSingleton<ICurrency, Stratis>()
                .AddSingleton<ICurrency, BitShares>()
                .AddSingleton<ICurrency, Siacoin>()
                .AddSingleton<ICurrency, AdEx>()
                .AddSingleton<ICurrency, Waves>()
                .AddSingleton<ICurrency, Golem>()
                .AddSingleton<ICurrency, Status>()
                .AddSingleton<ICurrency, DigixDAO>()
                .AddSingleton<ICurrency, Augur>()
                .AddSingleton<ICurrency, Zrx>()
                .AddSingleton<ICurrency, IOStoken>()
                .AddSingleton<ICurrency, Nano>()
                .AddSingleton<ICurrency, BasicAttentionToken>()
                .AddSingleton<ICurrency, Monaco>()
                .AddSingleton<ICurrency, Steem>()
                .AddSingleton<ICurrency, Civic>()
                .AddSingleton<ICurrency, Aelf>()
                .AddSingleton<ICurrency, PowerLedger>()
                .AddSingleton<ICurrency, Poet>()
                .AddSingleton<ICurrency, Cindicator>()
                .AddSingleton<ICurrency, Storj>()
                .AddSingleton<ICurrency, Decentraland>()
                .AddSingleton<ICurrency, Rcoin>()
                .AddSingleton<ICurrency, FunFair>()
                .AddSingleton<ICurrency, Syscoin>()
                .AddSingleton<ICurrency, Ark>()
                .AddSingleton<ICurrency, Enigma>()
                .AddSingleton<ICurrency, Walton>()
                .AddSingleton<ICurrency, Ontology>()
                .AddSingleton<ICurrency, NAVCoin>()
                .AddSingleton<ICurrency, BitcoinDiamond>()
                .AddSingleton<ICurrency, Tierion>()
                .AddSingleton<ICurrency, TimeNewBank>()
                .AddSingleton<ICurrency, district0x>()
                .AddSingleton<ICurrency, Bancor>()
                .AddSingleton<ICurrency, Gas>()
                .AddSingleton<ICurrency, BlockMason>()
                .AddSingleton<ICurrency, NucleusVision>()
                .AddSingleton<ICurrency, Decred>()
                .AddSingleton<ICurrency, Ardor>()
                .AddSingleton<ICurrency, Neblio>()
                .AddSingleton<ICurrency, Viberate>()
                .AddSingleton<ICurrency, RequestNetwork>()
                .AddSingleton<ICurrency, Gifto>()
                .AddSingleton<ICurrency, AppCoins>()
                .AddSingleton<ICurrency, Viacoin>()
                .AddSingleton<ICurrency, Quantstamp>()
                .AddSingleton<ICurrency, ETHLend>()
                .AddSingleton<ICurrency, Komodo>()
                .AddSingleton<ICurrency, SingularDTV>()
                .AddSingleton<ICurrency, ChainLink>()
                .AddSingleton<ICurrency, ZCoin>()
                .AddSingleton<ICurrency, Lunyr>()
                .AddSingleton<ICurrency, Zilliqa>()
                .AddSingleton<ICurrency, PIVX>()
                .AddSingleton<ICurrency, KyberNetwork>()
                .AddSingleton<ICurrency, AirSwap>()
                .AddSingleton<ICurrency, CyberMiles>()
                .AddSingleton<ICurrency, SimpleToken>()
                .AddSingleton<ICurrency, Nebulas>()
                .AddSingleton<ICurrency, VIBE>()
                .AddSingleton<ICurrency, Aion>()
                .AddSingleton<ICurrency, Storm>()
                .AddSingleton<ICurrency, Bluzelle>()
                .AddSingleton<ICurrency, SONM>()
                .AddSingleton<ICurrency, iExecRLC>()
                .AddSingleton<ICurrency, EnjinCoin>()
                .AddSingleton<ICurrency, Eidoo>()
                .AddSingleton<ICurrency, GenesisVision>()
                .AddSingleton<ICurrency, Loopring>()
                .AddSingleton<ICurrency, CoinDash>()
                .AddSingleton<ICurrency, INSEcosystem>()
                .AddSingleton<ICurrency, Everex>()
                .AddSingleton<ICurrency, Groestlcoin>()
                .AddSingleton<ICurrency, RaidenNetworkToken>()
                .AddSingleton<ICurrency, ThetaToken>()
                .AddSingleton<ICurrency, Aeternity>()
                .AddSingleton<ICurrency, Nuls>()
                .AddSingleton<ICurrency, Populous>()
                .AddSingleton<ICurrency, Nexus>()
                .AddSingleton<ICurrency, Ambrosus>()
                .AddSingleton<ICurrency, Aeron>()
                .AddSingleton<ICurrency, ZenCash>()
                .AddSingleton<ICurrency, WaBi>()
                .AddSingleton<ICurrency, Etherparty>()
                .AddSingleton<ICurrency, OpenAnx>()
                .AddSingleton<ICurrency, POANetwork>()
                .AddSingleton<ICurrency, Bread>()
                .AddSingleton<ICurrency, Agrello>()
                .AddSingleton<ICurrency, Monetha>()
                .AddSingleton<ICurrency, Mithril>()
                .AddSingleton<ICurrency, WePower>()
                .AddSingleton<ICurrency, GXShares>()
                .AddSingleton<ICurrency, MoedaLoyaltyPoints>()
                .AddSingleton<ICurrency, Dent>()
                .AddSingleton<ICurrency, Skycoin>()
                .AddSingleton<ICurrency, QLINK>()
                .AddSingleton<ICurrency, SingularityNET>()
                .AddSingleton<ICurrency, RepublicProtocol>()
                .AddSingleton<ICurrency, Selfkey>()
                .AddSingleton<ICurrency, TrueUSD>()
                .AddSingleton<ICurrency, StreamrDATAcoin>()
                .AddSingleton<ICurrency, Polymath>()
                .AddSingleton<ICurrency, PhoenixCoin>()
                .AddSingleton<ICurrency, VeChain>()
                .AddSingleton<ICurrency, Dock>()
                .AddSingleton<ICurrency, GoChain>()
                .AddSingleton<ICurrency, onGsocial>()
                .AddSingleton<ICurrency, PaxosStandardToken>()
                .AddSingleton<ICurrency, HyperCash>()
                .AddSingleton<ICurrency, IoTeX>()
                .AddSingleton<ICurrency, Mainframe>()
                .AddSingleton<ICurrency, LoomNetwork>()
                .AddSingleton<ICurrency, Holo>()
                .AddSingleton<ICurrency, PundiX>()
                .AddSingleton<ICurrency, MalteseLira>()
                .AddSingleton<ICurrency, StableUSD>()
                .AddSingleton<ICurrency, Ravencoin>()
                .AddSingleton<ICurrency, QuarkChain>()
                .AddSingleton<ICurrency, USDC>()
                .AddSingleton<ICurrency, Wanchain>()
                .AddSingleton<ICurrency, Yoyow>()
                .AddSingleton<ICurrency, Ethos>()
                .AddSingleton<ICurrencyFactory, CurrencyFactory>();

            // Symbol factory
            serviceCollection.AddSingleton<ISymbolFactory, SymbolFactory>();

            serviceCollection.AddSingleton<IExchangeTradeStatProvider, ExchangeTradeStatProvider>();

            // Order Side factory
            serviceCollection.AddSingleton<IOrderSideFactory, OrderSideFactory>();

            return serviceCollection;
        }
    }
}