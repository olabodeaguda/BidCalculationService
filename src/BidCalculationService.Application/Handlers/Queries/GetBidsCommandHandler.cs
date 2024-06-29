using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Interfaces.Services;
using BidCalculationService.Domain.Models;
using MediatR;

namespace BidCalculationService.Application.Handlers.Queries
{
    public class GetBidsRequest : IRequest<Pageable<BidResponseDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetBidsCommandHandler : IRequestHandler<GetBidsRequest, Pageable<BidResponseDto>>
    {
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public GetBidsCommandHandler(IBidRepository bidRepository, IMapper mapper, IClaimService claimService)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
            _claimService = claimService;
        }
        public async Task<Pageable<BidResponseDto>> Handle(GetBidsRequest request, CancellationToken cancellationToken)
        {
            Guid? userId = _claimService.GetLogOnUserId();
            if (!userId.HasValue)
            {
                return new Pageable<BidResponseDto>
                {
                    Items = Array.Empty<BidResponseDto>(),
                    PageNumber = 0,
                    PageSize = 0,
                    TotalItems = 0
                };
            }

            Pageable<Bid> bids = await _bidRepository.GetBidsPaginatedAsync(request.PageNumber, request.PageSize, userId.Value);
            return new Pageable<BidResponseDto>
            {
                Items = _mapper.Map<BidResponseDto[]>(bids.Items),
                PageNumber = bids.PageNumber,
                PageSize = bids.PageSize,
                TotalItems = bids.TotalItems
            };
        }
    }
}
