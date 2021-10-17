using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoCurrency.Repository.Edm.Historian
{
    [Table("interval")]
    public class IntervalEntity
    {
        [Required]
        [Column("interval