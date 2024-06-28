namespace BidCalculationService.Domain.Entities
{
    public class ApplicationUser
    {
        public Guid Id { get; set; } = default!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;

        public ICollection<Bid> Bids { get; set; }

        public ApplicationUser()
        {
            Bids = new HashSet<Bid>();
        }
    }
}
