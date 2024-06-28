using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Interfaces.Repositories;
using MediatR;

namespace BidCalculationService.Application.Handlers.Queries
{
    public class GetBidRequest : IRequest<Result<BidResponseDto>>
    {
        public long Id { get; set; }
    }

    public class GetBidCommandHandler : IRequestHandler<GetBidRequest, Result<BidResponseDto>>
    {
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public GetBidCommandHandler(IBidRepository bidRepository, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        public async Task<Result<BidResponseDto>> Handle(GetBidRequest request, CancellationToken cancellationToken)
        {
            var bid = await _bidRepository.GetAsync(request.Id);
            if (bid == null)
                return Result<BidResponseDto>.Failure(BidError.BidFailed());

            return Result<BidResponseDto>.Success(_mapper.Map<BidResponseDto>(bid));
        }
    }
}
