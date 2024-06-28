using BidCalculationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> CreateAsync(ApplicationUser applicationUser);
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}
