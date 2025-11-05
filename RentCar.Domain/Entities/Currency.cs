using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("Currency")]
    public class Currency
    {
        [Key]
        [StringLength(3)]
        [Column("CurrencyId", TypeName = "char(3)")]
        public string CurrencyId { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [Column("CurrencyName")]
        public string CurrencyName { get; set; } = null!;

        [MaxLength(3)]
        [Column("Symbol")]
        public string? Symbol { get; set; }

        public ICollection<Car> Cars { get; set; } = [];
        
        public ICollection<Rental> Rentals { get; set; } = [];
    }
}
