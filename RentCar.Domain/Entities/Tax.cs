using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("Tax")]
    public class Tax
    {
        [Key]
        [Column("TaxId")]
        public int TaxId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("TaxName")]
        public string TaxName { get; set; } = null!;

        [MaxLength(255)]
        [Column("TaxDescription")]
        public string? TaxDescription { get; set; }

        [Range(0, 100)]
        [Column("Rate", TypeName = "decimal(5,2)")]
        public decimal Rate { get; set; }

        public ICollection<Rental> Rentals { get; set; } = [];
    }
}
