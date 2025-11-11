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
        public string CurrencyId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("CurrencyName")]
        public string CurrencyName { get; set; } = string.Empty;

        [MaxLength(3)]
        [Column("Symbol")]
        public string? Symbol { get; set; }

        [InverseProperty(nameof(Car.Currency))]
        public ICollection<Car>? Cars { get; set; }
        
        [InverseProperty(nameof(Rental.Currency))]
        public ICollection<Rental>? Rentals { get; set; }
    }
}
