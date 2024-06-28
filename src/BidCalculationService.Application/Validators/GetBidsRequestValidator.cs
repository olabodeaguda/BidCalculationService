using BidCalculationService.Application.Handlers.Queries;
using FluentValidation;

namespace BidCalculationService.Application.Validators
{
    public class GetBidsRequestValidator : AbstractValidator<GetBidsRequest>
    {
        public GetBidsRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number is required");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page number is required");
        }
    }
}
