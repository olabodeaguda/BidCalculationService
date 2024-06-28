using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Commands;
using FluentValidation;

namespace BidCalculationService.Application.Validators
{
    public class CreateBidRequestValidator : AbstractValidator<CreateBidRequest>
    {
        public CreateBidRequestValidator()
        {
            RuleFor(x => x.VehicleType)
                .NotEmpty()
                .NotEqual(VehicleTypeDto.NONE)
                .WithMessage("Vehicle type must not be None.");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0)
                .WithMessage("Base price must be greater than zero.");
        }
    }
}
