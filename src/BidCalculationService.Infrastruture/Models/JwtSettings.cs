namespace BidCalculationService.Infrastruture.Models
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public long ExpiresinSeconds { get; set; }
    }
}
