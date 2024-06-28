using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Application.Validators;
using FluentValidation.TestHelper;

namespace BidCalculationService.test.Applications.Validators
{
    public class CreateBidRequestValidatorTests
    {
        private CreateBidRequestValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateBidRequestValidator();
        }

        [Test]
        public void Should_Have_Error_When_VehicleType_Is_Empty()
        {
            var model = new CreateBidRequest { VehicleType = VehicleTypeDto.NONE };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.VehicleType)
                .WithErrorMessage("Vehicle type must not be None.");
        }

        [Test]
        public void Should_Have_Error_When_BasePrice_Is_Zero()
        {
            var model = new CreateBidRequest { BasePrice = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.BasePrice)
                .WithErrorMessage("Base price must be greater than zero.");
        }

        [Test]
        public void Should_Have_Error_When_BasePrice_Is_Negative()
        {
            var model = new CreateBidRequest { BasePrice = -10 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.BasePrice)
                .WithErrorMessage("Base price must be greater than zero.");
        }

        [Test]
        public void Should_Not_Have_Error_When_Valid()
        {
            var model = new CreateBidRequest
            {
                VehicleType = VehicleTypeDto.COMMON,
                BasePrice = 100
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
