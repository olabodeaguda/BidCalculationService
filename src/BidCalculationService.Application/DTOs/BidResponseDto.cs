namespace BidCalculationService.Application.DTOs
{
    public class BidResponseDto
    {
        public long Id { get; set; }
        public string VehicleType { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalFees
        {
            get
            {
                return Fees.Sum(fee => fee.Amount);
            }
        }
        public FeeDto[] Fees { get; set; } = Array.Empty<FeeDto>();
    }
}
