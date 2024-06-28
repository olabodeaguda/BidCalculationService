
using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Application.Validators;
using FluentValidation.TestHelper;

namespace BidCalculationService.test.Applications.Validators
{
    public class CreateAccountRequestValidatorTests
    {
        private CreateAccountRequestValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateAccountRequestValidator();
        }

        [Test]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var model = new CreateAccountRequest { Email = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email is required.");
        }

        [Test]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var model = new CreateAccountRequest { Email = "invalidemail" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Invalid email format.");
        }

        [Test]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var model = new CreateAccountRequest { Password = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Password is required.");
        }

        [Test]
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
            var model = new CreateAccountRequest { Password = "short" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Password must be at least 6 characters long.");
        }

        [Test]
        public void Should_Have_Error_When_Password_Does_Not_Meet_Criteria()
        {
            var model = new CreateAccountRequest { Password = "password" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Password must be at least 6 characters long, contain at least one letter, one number, and one special character.");
        }

        [Test]
        public void Should_Have_Error_When_ConfirmPassword_Is_Empty()
        {
            var model = new CreateAccountRequest { ConfirmPassword = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword)
                .WithErrorMessage("Confirm Password is required.");
        }

        [Test]
        public void Should_Have_Error_When_Passwords_Do_Not_Match()
        {
            var model = new CreateAccountRequest { Password = "Password1@", ConfirmPassword = "Password2@" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword)
                .WithErrorMessage("Passwords do not match.");
        }

        [Test]
        public void Should_Not_Have_Error_When_Valid()
        {
            var model = new CreateAccountRequest
            {
                Email = "test@example.com",
                Password = "Password1@",
                ConfirmPassword = "Password1@"
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
