using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Interfaces.Services;
using MediatR;

namespace BidCalculationService.Application.Handlers.Commands
{
    public class CreateBidRequest : IRequest<Result<BidResponseDto>>
    {
        public VehicleTypeDto VehicleType { get; set; }
        public decimal BasePrice { get; set; }
    }

    public class CreateBidCommandHandler : IRequestHandler<CreateBidRequest, Result<BidResponseDto>>
    {
        private IFeeCalculatorService _feeCalculatorService;
        private IBidRepository _bidRepository;
        private IMapper _mapper;
        private readonly IClaimService _claimService;

        public CreateBidCommandHandler(IBidRepository bidRepository, IMapper mapper,
            IFeeCalculatorService feeCalculatorService, IClaimService claimService)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
            _feeCalculatorService = feeCalculatorService;
            _claimService = claimService;
        }

        public async Task<Result<BidResponseDto>> Handle(CreateBidRequest request, CancellationToken cancellationToken)
        {
            Guid? userId = _claimService.GetLogOnUserId();
            if (!userId.HasValue)
                return Result<BidResponseDto>.Failure(BidError.UserNotFound());

            Bid? bid = _mapper.Map<Bid>(request);

            Result<List<Fee>> fees = _feeCalculatorService.CalculateTotalFeesAsync(bid.VehicleType, bid.BasePrice);
            if (!fees.IsSuccess) return Result<BidResponseDto>.Failure(BidError.FeeCalculationFailed());

            bid.Fees = fees.Data!;
            bid = await _bidRepository.CreateAsync(bid, userId.Value);
            if (bid == null)
                return Result<BidResponseDto>.Failure(BidError.BidCalculationFailed());

            return Result<BidResponseDto>.Success(_mapper.Map<BidResponseDto>(bid));
        }
    }
}
