using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Models;

namespace BidCalculationService.Domain.Interfaces.Repositories
{
    public interface IBidRepository
    {
        Task<Bid?> CreateAsync(Bid bid, Guid value);
        Task<Bid?> GetAsync(long id);
        Task<Pageable<Bid>> GetBidsPaginatedAsync(int pageNumber, int pageSize, Guid userId);
    }
}
