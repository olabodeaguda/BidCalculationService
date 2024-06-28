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
    public class GetBidsRequestValidatorTests
    {
        private GetBidsRequestValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new GetBidsRequestValidator();
        }

        [Test]
        public void Should_Have_Error_When_PageNumber_Is_Zero()
        {
            var model = new GetBidsRequest { PageNumber = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PageNumber)
                .WithErrorMessage("Page number is required");
        }

        [Test]
        public void Should_Have_Error_When_PageSize_Is_Zero()
        {
            var model = new GetBidsRequest { PageSize = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PageSize)
                .WithErrorMessage("Page number is required");
        }

        [Test]
        public void Should_Not_Have_Error_When_PageNumber_And_PageSize_Are_Positive()
        {
            var model = new GetBidsRequest { PageNumber = 1, PageSize = 10 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
