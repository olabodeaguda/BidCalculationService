using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Interfaces.Services;
using BidCalculationService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BidCalculationService.Infrastruture.Data.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;
        private readonly IClaimService _claimService;
        public BidRepository(AppDbContext appDbContext, ILogger<BidRepository> logger, IClaimService claimService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _claimService = claimService;
        }

        public async Task<Bid?> CreateAsync(Bid bid)
        {
            try
            {
                bid.BidBy = (Guid)_claimService.GetLogOnUserId()!;
                _appDbContext.Bids.Add(bid);
                await _appDbContext.SaveChangesAsync();
                return bid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating bid");
                return null;
            }
        }

        public async Task<Bid?> GetAsync(long id)
            => await _appDbContext.Bids.Include(_ => _.Fees).SingleOrDefaultAsync(b => b.Id == id);

        public async Task<Pageable<Bid>> GetBidsPaginatedAsync(int pageNumber, int pageSize)
        {
            var count = await _appDbContext.Bids.CountAsync();
            var entities = await _appDbContext.Bids.Include(_ => _.Fees)
                .OrderByDescending(_ => _.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();

            return new Pageable<Bid>
            {
                Items = entities,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalItems = count
            };
        }
    }
}
