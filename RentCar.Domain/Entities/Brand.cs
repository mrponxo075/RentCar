using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("Brand")]
    public class Brand
    {
        [Key]
        [Column("BrandId")]
        public int BrandId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("BrandName")]
        public string BrandName { get; set; } = string.Empty;

        [InverseProperty(nameof(Model.Brand))]
        public ICollection<Model>? Models { get; set; }

        [InverseProperty(nameof(Model.Car))]
        public ICollection<Car>? Cars { get; set; }
    }
}
