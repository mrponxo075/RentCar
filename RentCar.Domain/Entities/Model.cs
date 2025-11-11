using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("Model")]
    public class Model
    {
        [Key]
        [Column("ModelId")]
        public int ModelId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("ModelName")]
        public string ModelName { get; set; } = string.Empty;

        [Required]
        [Column("BrandId")]
        public int BrandId { get; set; }

        [Required]
        [Column("FuelTypeId")]
        public int FuelTypeId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand? Brand { get; set; }

        [ForeignKey(nameof(FuelTypeId))]
        public FuelType? FuelType { get; set; }

        [InverseProperty(nameof(Car.Model))]
        public Car? Car { get; set; }
    }
}
