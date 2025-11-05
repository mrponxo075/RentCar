using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("Car")]
    public class Car
    {
        [Key]
        [Column("CarId")]
        public int CarId { get; set; }

        [Required]
        [Column("ModelId")]
        public int ModelId { get; set; }

        [Required]
        [Column("BrandId")]
        public int BrandId { get; set; }

        [Range(1980, short.MaxValue)]
        [Column("ManufacterYear")]
        public short ManufacterYear { get; set; } = 1980;

        [Required]
        [MaxLength(20)]
        [Column("LicensePlate", TypeName = "varchar(20)")]
        public string LicensePlate { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [Column("Color")]
        public string Color { get; set; } = null!;

        [Range(0, double.MaxValue)]
        [Column("PricePerDay", TypeName = "decimal(10,2)")]
        public decimal PricePerDay { get; set; }

        [Range(0, double.MaxValue)]
        [Column("PricePerWeek", TypeName = "decimal(10,2)")]
        public decimal PricePerWeek { get; set; }

        [Range(0, double.MaxValue)]
        [Column("PricePerHour", TypeName = "decimal(10,2)")]
        public decimal PricePerHour { get; set; }

        [Required]
        [StringLength(3)]
        [Column("CurrencyId", TypeName = "char(3)")]
        public string CurrencyId { get; set; } = null!;

        [Range(0, short.MaxValue)]
        [Column("Stock")]
        public short Stock { get; set; }

        [ForeignKey(nameof(ModelId))]
        public Model Model { get; set; } = null!;

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; } = null!;

        [ForeignKey(nameof(CurrencyId))]
        public Currency Currency { get; set; } = null!;

        public ICollection<Rental> Rentals { get; set; } = [];
    }
}
