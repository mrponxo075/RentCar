using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [Column("CustomerId")]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("LastName")]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("SecondLastName")]
        public string? SecondLastName { get; set; }

        [MaxLength(150)]
        [EmailAddress]
        [Column("Email")]
        public string? Email { get; set; }

        [Required]
        [MaxLength(20)]
        [Phone]
        [Column("Phone", TypeName = "varchar(20)")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("CustomerAddress")]
        public string CustomerAddress { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("IDNumber")]
        public string? IDNumber { get; set; }

        [Column("IDCardTypeId")]
        public int? IDCardTypeId { get; set; }

        [ForeignKey(nameof(IDCardTypeId))]
        public IDCardType? IDCardType { get; set; }

        [InverseProperty(nameof(Rental.Customer))]
        public ICollection<Rental>? Rentals { get; set; }
    }
}
