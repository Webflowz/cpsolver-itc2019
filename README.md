# cryptocurrency trading platform
[![Build Status](https://travis-ci.com/smnplicity/cryptocurrency.svg?token=6dFz7PgYHV1ppXKK86sV&branch=master)](https://travis-ci.com/smnplicity/cryptocurrency)

## Features
* Collects cryptocurrency trades from exchanges and runs ohlc (and a bit more) aggregation on it
	* Including historical trades
* Standardised interaction across all supported exchanges
* The trades and trade aggregations are stored in a MySQL database
* Provides various market indicators such as RSI and MACD
* Supports private exchange API functionality e.g. placing orders

## Supported Exchanges

Exchange | Https | Web Socket
-------- | :-----: | :-----------:
Bitfinex | :heavy_check_mark: | :heavy_check_mark:
Binance | :heavy_check_mark: | :heavy_check_mark:
CoinbasePro | :heavy_check_mark:
Kraken | :heavy_check_mark: | :heavy_check_mark:

## Supported Symbols
Note: The symbols supported by exchange will differ.

Currently supports over 400 symbols including:

Symbol | Description
-------- | -----
BTCAUD | Bitcoin / Australian Dollar
BTCUSD | Bitcoin / U.S Dollar
BTCUSDLONGS | Open long positions in BTCUSD on Bitfinex
BTCUSDSHORTS | Open short positions in BTCUSD on Bitfinex 
BTCUSDT | Bitcoin / Tether USD
ETCAUD | Ethereum Classic / Australian Dollar
ETHAUD | Ethereum / Australian Dollar
ETHBTC | Ethereum / Bitcoin
ETHBTCLONGS | Open long positions in ETHBTC on Bitfinex 
ETHBTCSHORTS | Open short positions in ETHBTC on Bitfinex
ETHUSD | Ethereum / U.S. Dollar
ETHUSDLONGS | Open long positions in ETHUSD on Bitfinex
ETHUSDSHORTS | Open short positions in BTCUSD on Bitfinex 
ETHUSDT | Ethereum / Tether USD
LTCAUD | Litecoin / Australian Dollar
LTCBTC | Litecoin / Bitcoin
LTCBTCLONGS | Open long positions in LTCBTC on Bitfinex 
LTCBTCSHORTS | Open short positions in LTCBTC on Bitfinex 
LTCUSD | Litecoin / U.S. Dollar
LTCUSDLONGS | Open long positions in LTCUSD on Bitfinex 
LTCUSDSHORTS | Open short positions in LTCUSD on Bitfinex 
LTCUSDT | Litecoin / Tether USD
XLMBTC | Stellar Lumens / Bitcoin
XLMETH | Stellar Lumens / Ethereum

## Supported OHLC intervals
Interval Key | Label
-------------|----------
1m | 1 minute
3m | 3 minutes
5m | 5 minutes
15m | 15 minutes
30m | 30 minutes
1h | 1 hour
2h | 2 hours
3h | 3 hours
4h | 4 hours
6h | 6 hours
12h | 12 hours
1D | 1 day
1W | 1 week
1M | 1 month
	
## Historian Service
### Installation
1. Run the database script found in src\CryptoCurrency.HistorianService\create_historian.sql on a MySQL instance
2. Create a user in MySQL with the following permissions on the schema: CREATE TEMPORARY TABLES, DELETE, EXECUTE, GRANT OPTION, INSERT, LOCK TABLES, SELECT, SHOW VIEW, UPDATE
3. In src\CryptoCurrency.HistorianService\ap