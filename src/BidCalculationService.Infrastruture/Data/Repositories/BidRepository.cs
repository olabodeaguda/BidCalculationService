using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BidCalculationService.Infrastruture.Data.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;
        public BidRepository(AppDbContext appDbContext, ILogger<BidRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Bid?> CreateAsync(Bid bid, Guid userId)
        {
            try
            {
                bid.BidBy = userId;
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

        public async Task<Pageable<Bid>> GetBidsPaginatedAsync(int pageNumber, int pageSize, Guid userId)
        {
            var query = _appDbContext.Bids.Include(_ => _.Fees)
                .Where(_ => _.BidBy == userId)
                .AsQueryable();

            var count = await query.CountAsync();
            var entities = await query.Include(_ => _.Fees)
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
