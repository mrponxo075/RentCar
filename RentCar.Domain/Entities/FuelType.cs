using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("FuelType")]
    public class FuelType
    {
        [Key]
        [Column("FuelTypeId")]
        public int FuelTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("FuelTypeName")]
        public string FuelTypeName { get; set; } = null!;

        [MaxLength(255)]
        [Column("FuelTypeDescription")]
        public string? FuelTypeDescription { get; set; }

        [Range(0, short.MaxValue)]
        [Column("KilometersAutonomy")]
        public short KilometersAutonomy { get; set; }

        public ICollection<Model> Models { get; set; } = [];
    }
}
