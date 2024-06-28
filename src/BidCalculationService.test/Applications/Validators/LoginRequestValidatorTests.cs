using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Application.Validators;
using FluentValidation.TestHelper;

namespace BidCalculationService.test.Applications.Validators
{
    public class LoginRequestValidatorTests
    {
        private LoginRequestValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new LoginRequestValidator();
        }

        [Test]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var model = new LoginRequest { Email = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email is required.");
        }

        [Test]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var model = new LoginRequest { Password = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Password is required.");
        }

        [Test]
        public void Should_Not_Have_Error_When_Valid()
        {
            var model = new LoginRequest
            {
                Email = "test@test.com",
                Password = "@Password0"
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
