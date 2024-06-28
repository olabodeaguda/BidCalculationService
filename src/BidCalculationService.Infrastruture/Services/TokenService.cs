using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Interfaces.Services;
using BidCalculationService.Infrastruture.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.Infrastruture.Services
{
    internal class TokenService : ITokenService
    {
        private const long expirationSeconds = 3600;

        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<TokenService> _logger;
        public TokenService(JwtSettings ssoIdentityConfig, ILogger<TokenService> logger)
        {
            _jwtSettings = ssoIdentityConfig;
            _logger = logger;
        }

        public Result<(string token, long expires)> GenerateToken(ApplicationUser user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

                var claimsIdentity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Name, user.LastName),
            });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.ExpiresinSeconds),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Result<(string token, long expires)>.Success((tokenHandler.WriteToken(token), expirationSeconds));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating token");
                return Result<(string token, long expires)>.Failure(TokenServiceError.TokenGenerationFailed());
            }
        }
    }
}
