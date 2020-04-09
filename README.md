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
--------