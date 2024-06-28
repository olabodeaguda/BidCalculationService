using BidCalculationService.Application.Handlers.Queries;
using BidCalculationService.Application.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.test.Applications.Validators
{
    public class GetBidRequestValidatorTests
    {
        private GetBidRequestValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new GetBidRequestValidator();
        }

        [Test]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            var model = new GetBidRequest { Id = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Bid Id is required");
        }

        [Test]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            var model = new GetBidRequest { Id = -1 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Bid Id is required");
        }

        [Test]
        public void Should_Not_Have_Error_When_Id_Is_Positive()
        {
            var model = new GetBidRequest { Id = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
