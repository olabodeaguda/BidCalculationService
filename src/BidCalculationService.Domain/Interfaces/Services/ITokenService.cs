using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Result<(string token, long expires)> GenerateToken(ApplicationUser user);
    }
}
