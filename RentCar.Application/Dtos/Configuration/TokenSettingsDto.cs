namespace RentCar.Application.Dtos.Configuration
{
    public class TokenSettingsDto
    {
        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;
    }
}
