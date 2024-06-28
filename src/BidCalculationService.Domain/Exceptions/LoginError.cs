using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.Domain.Exceptions
{
    public class LoginError
    {
        public static Error InvalidCredentials() => new("Invalid_Credentials", "Email or password is invalid");
    }
}
