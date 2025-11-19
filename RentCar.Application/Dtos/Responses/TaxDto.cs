namespace RentCar.Application.Dtos.Responses
{
    public class TaxDto
    {
        public int TaxId { get; set; }

        public string TaxName { get; set; } = string.Empty;

        public string? TaxDescription { get; set; }

        public decimal Rate { get; set; }
    }
}
