using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("IDCardType")]
    public class IDCardType
    {
        [Key]
        [Column("IDCardTypeId")]
        public int IDCardTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("TypeName")]
        public string TypeName { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column("IDCardTypeDescription")]
        public string? IDCardTypeDescription { get; set; }

        [InverseProperty(nameof(Customer.IDCardType))]
        public ICollection<Customer>? Customers { get; set; }
    }
}
