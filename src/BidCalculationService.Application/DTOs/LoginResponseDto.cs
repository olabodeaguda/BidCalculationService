namespace BidCalculationService.Application.DTOs
{
    public class UserProfile
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public long Expires { get; set; }
        public string Token { get; set; } = string.Empty;
        public UserProfile UserProfile { get; set; } = new UserProfile();
    }
}
