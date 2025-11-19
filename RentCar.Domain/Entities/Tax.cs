using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Domain.Entities
{
    public enum TaxType
    {
        IVA_REGULAR = 1,
        IVA_REDUCED = 2,
    }

    [Table("Tax")]
    public class Tax
    {
        [Key]
        [Column("TaxId")]
        public int TaxId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("TaxName")]
        public string TaxName { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column("TaxDescription")]
        public string? TaxDescription { get; set; }

        [Range(0, 100)]
        [Column("Rate", TypeName = "decimal(5,2)")]
        public decimal Rate { get; set; }

        [InverseProperty(nameof(Rental.Tax))]
        public ICollection<Rental>? Rentals { get; set; }

        public bool IsRemovable()
        {
            return !IsPermanentTax(TaxId)
                && Rentals != null
                && Rentals!.Count == 0;
        }

        public static bool IsPermanentTax(int taxId)
        {
            return TaxType.IVA_REGULAR.Equals(taxId)
                || TaxType.IVA_REDUCED.Equals(taxId);
        }
    }
}
