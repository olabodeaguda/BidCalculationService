
namespace BidCalculationService.Domain.Exceptions
{
    public class BidError
    {
        public static Error BidCalculationFailed() => new("Request.Failed", "An error occur while processing your request");
        public static Error FeeCalculationFailed() => new("Fee.Failed", "An error occur while calculating fee");
        public static Error BidFailed() => new("Bid.NotFound", "Bid not found");
        public static Error UserNotFound() => new("User.NotFound", "User not found");
      
    }
}
