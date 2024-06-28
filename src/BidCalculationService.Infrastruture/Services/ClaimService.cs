using BidCalculationService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BidCalculationService.Infrastruture.Services
{
    public class ClaimService : IClaimService
    {
        private readonly HttpContext? _httpContext;
        private readonly ILogger<ClaimService> _logger;
        public ClaimService(IHttpContextAccessor httpContext, ILogger<ClaimService> logger)
        {
            _httpContext = httpContext.HttpContext;
            _logger = logger;
        }

        public string? GetLogOnUserEmail()
        {
            try
            {
                if (_httpContext?.User == null) return null;
                var email = _httpContext?.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (email == null) return null;

                return email.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetLogOnUserEmail)}:Error getting log on user details");
                return null;
            }
        }

        public Guid? GetLogOnUserId()
        {
            try
            {
                if (_httpContext?.User == null) return null;
                var sub = _httpContext?.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (sub == null) return null;

                bool isValid = Guid.TryParse(sub.Value, out var userId);
                return isValid ? userId : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetLogOnUserId)}:Error getting log on user details");
                return null;
            }
        }
    }
}
