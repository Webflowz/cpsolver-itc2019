using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using CryptoCurrency.Core;
using CryptoCurrency.Core.Currency;
using CryptoCurrency.Core.Exchange.Model;
using CryptoCurrency.Core.Extensions;
using CryptoCurrency.Core.Market;
using CryptoCurrency.Core.OrderSide;
using CryptoCurrency.Core.Symbol;

using CryptoCurrency.ExchangeClient.CoinbasePro.Model;

namespace CryptoCurrency.ExchangeClient.CoinbasePro
{
    public static class TypeConverter
    {
        public static T2 ChangeType<T, T2>(this CoinbasePro exchange, ICurrencyFactory currencyFactory, ISymbolFactory symbolFactory, object requestData, T obj, long? cbBefore)
        {
            if (typeof(T) == typeof(CoinbaseProCancelOrder))
                return (T2)(object)new CancelOrder();

            if (typeof(T) == typeof(CoinbaseProCreateOrderResponse))
            {
                var order = obj as CoinbaseProCreateOrderResponse;

                var symbol = exchange.DecodeProductId(order.ProductId);

                return (T2)(object)new CreateOrder
                {
                    Id = order.Id,
                    SymbolCode = symbol.Code,
                    Side = exchange.GetOrderSide(order.Side),
                    Type = exchange.GetOrderType(order.Type),
                    OrderEpoch = new Epoch(order.CreatedAt.ToUniversalTime()),
                    Price = order.Price,
                    Volume = order.Size,
                    State = exchange.GetOrderState(order.Status)
                };
     