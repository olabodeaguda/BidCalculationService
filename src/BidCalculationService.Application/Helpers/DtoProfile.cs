using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Domain.Entities;

namespace BidCalculationService.Application.Helpers
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<CreateAccountRequest, ApplicationUser>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.ToSha256()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<ApplicationUser, CreateAccountResponseDto>();
            CreateMap<ApplicationUser, UserProfile>();
            CreateMap<CreateBidRequest, Bid>();
            CreateMap<VehicleTypeDto, VehicleType>().ReverseMap();
            CreateMap<FeeTypeDto, FeeType>().ReverseMap();
            CreateMap<Fee, FeeDto>()
                .ForMember(_ => _.FeeType, opt => opt.MapFrom(src => src.FeeType.ToString()));
            CreateMap<Bid, BidResponseDto>()
                .ForMember(_ => _.VehicleType, opt => opt.MapFrom(src => src.VehicleType.ToString()));
        }
    }
}
