using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("Rental")]
    public class Rental
    {
        [Key]
        [Column("RentalId")]
        public int RentalId { get; set; }

        [Required]
        [Column("CarId")]
        public int CarId { get; set; }

        [Required]
        [Column("CustomerId")]
        public int CustomerId { get; set; }

        [Required]
        [Column("TaxId")]
        public int TaxId { get; set; }

        [Required]
        [StringLength(3)]
        [Column("CurrencyId", TypeName = "char(3)")]
        public string CurrencyId { get; set; } = null!;

        [Required]
        [Column("StartDate", TypeName = "datetime2(0)")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("EndDate", TypeName = "datetime2(0)")]
        public DateTime EndDate { get; set; }

        [Range(0, double.MaxValue)]
        [Column("RentalPrice", TypeName = "decimal(10,2)")]
        public decimal RentalPrice { get; set; }

        [Range(0, double.MaxValue)]
        [Column("AssurancePrice", TypeName = "decimal(10,2)")]
        public decimal? AssurancePrice { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [ForeignKey(nameof(TaxId))]
        public Tax Tax { get; set; } = null!;

        [ForeignKey(nameof(CurrencyId))]
        public Currency Currency { get; set; } = null!;
    }
}
