namespace BidCalculationService.Domain.Entities
{
    public class Fee
    {
        public long Id { get; set; }
        public FeeType FeeType { get; set; }
        public decimal Amount { get; set; }
        public long BidId { get; set; }

        public Bid Bid { get; set; } = null!;
    }
}
