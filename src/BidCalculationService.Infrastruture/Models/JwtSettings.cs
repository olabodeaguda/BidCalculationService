namespace BidCalculationService.Infrastruture.Models
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public long ExpiresinSeconds { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}
