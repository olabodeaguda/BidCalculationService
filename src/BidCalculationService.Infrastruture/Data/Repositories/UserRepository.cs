using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BidCalculationService.Infrastruture.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;
        public UserRepository(AppDbContext appDbContext, ILogger<UserRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<ApplicationUser?> CreateAsync(ApplicationUser applicationUser)
        {
            try
            {
                _appDbContext.Users.Add(applicationUser);
                await _appDbContext.SaveChangesAsync();
                return applicationUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user");
                return null;
            }
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Users.SingleOrDefaultAsync(_ => _.Email == email);
        }
    }
}
