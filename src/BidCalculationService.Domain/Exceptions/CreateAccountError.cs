using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.Domain.Exceptions
{
    public class CreateAccountError
    {
        public static Error AccountAlreadyExist() => new("Account.AlreadyExist", "Account already exist");
        public static Error AccountCreationFailed() => new("Account.AlreadyExist", "Account already exist");
    }
}
