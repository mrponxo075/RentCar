namespace RentCar.Application.Dtos.Requests
{
    public class TaxRequestDto
    {
        public string TaxName { get; set; } = string.Empty;

        public string? TaxDescription { get; set; }

        public decimal Rate { get; set; }
    }
}
