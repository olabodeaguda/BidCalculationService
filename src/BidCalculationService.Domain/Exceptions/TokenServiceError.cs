using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.Domain.Exceptions
{
    public class TokenServiceError
    {
        public static Error TokenGenerationFailed() => new("Token_Generation_Failed", "Token generation failed");
    }
}
