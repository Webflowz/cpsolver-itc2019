using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoCurrency.Repository.Edm.Historian
{
    [Table("exchange_trade")]
    public class ExchangeTradeEntity
    {
        [Column("exchange_id")]
        public int ExchangeId { get; set; }

        [Column("symbol_id")]
        public int SymbolId { get; set; }

        [Column("timestamp")]
        pu