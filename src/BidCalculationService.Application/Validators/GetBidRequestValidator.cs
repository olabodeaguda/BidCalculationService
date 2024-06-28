using BidCalculationService.Application.Handlers.Queries;
using FluentValidation;

namespace BidCalculationService.Application.Validators
{
    public class GetBidRequestValidator : AbstractValidator<GetBidRequest>
    {
        public GetBidRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Bid Id is required");
        }
    }
}
