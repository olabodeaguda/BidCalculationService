using AutoMapper;
using BidCalculationService.Application;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.test.Applications.Handlers
{
    public class LoginCommandHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ITokenService> _tokenServiceMock;
        private LoginCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _tokenServiceMock = new Mock<ITokenService>();
            _handler = new LoginCommandHandler(_userRepositoryMock.Object, _mapperMock.Object, _tokenServiceMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
        {
            var request = new LoginRequest { Email = "test@example.com", Password = "password" };
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser?)null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error!.Description, Is.EqualTo(LoginError.InvalidCredentials().Description));
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenPasswordIsIncorrect()
        {
            var request = FizzWare.NBuilder.Builder<LoginRequest>.CreateNew().Build();
            var user = FizzWare.NBuilder.Builder<ApplicationUser>.CreateNew().Build();
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error!.Description, Is.EqualTo(LoginError.InvalidCredentials().Description));
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenTokenGenerationFails()
        {
            var request = FizzWare.NBuilder.Builder<LoginRequest>.CreateNew().Build();
            var user = FizzWare.NBuilder.Builder<ApplicationUser>.CreateNew().Build();

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _tokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<ApplicationUser>())).Returns(Result<(string token, long expires)>.Failure(TokenServiceError.TokenGenerationFailed()));

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error!.Description, Is.EqualTo(LoginError.InvalidCredentials().Description));
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenLoginIsSuccessful()
        {
            var request = FizzWare.NBuilder.Builder<LoginRequest>.CreateNew().Build();
            var user = FizzWare.NBuilder.Builder<ApplicationUser>.CreateNew().Build();
            user.Password = request.Password.ToSha256();
            (string token, long expires) token = ("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", DateTime.UtcNow.AddHours(1).Second);
            var userProfile = FizzWare.NBuilder.Builder<UserProfile>.CreateNew().Build();
            var loginResponse = new LoginResponseDto { UserProfile = userProfile, Token = token.token, Expires = token.expires };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _tokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<ApplicationUser>())).Returns(Result<(string token, long expires)>.Success(token));
            _mapperMock.Setup(mapper => mapper.Map<UserProfile>(It.IsAny<ApplicationUser>())).Returns(userProfile);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Data!.UserProfile, Is.EqualTo(loginResponse.UserProfile));
            Assert.That(result.Data.Token, Is.EqualTo(loginResponse.Token));
            Assert.That(result.Data.Expires, Is.EqualTo(loginResponse.Expires));
        }
    }
}
