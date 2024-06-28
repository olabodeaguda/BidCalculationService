using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Interfaces.Repositories;
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

        public GetBidsCommandHandler(IBidRepository bidRepository, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }
        public async Task<Pageable<BidResponseDto>> Handle(GetBidsRequest request, CancellationToken cancellationToken)
        {
            Pageable<Bid> bids = await _bidRepository.GetBidsPaginatedAsync(request.PageNumber, request.PageSize);
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
