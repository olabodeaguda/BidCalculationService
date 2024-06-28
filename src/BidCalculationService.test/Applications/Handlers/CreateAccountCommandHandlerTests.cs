using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Interfaces.Repositories;
using FizzWare.NBuilder;
using Moq;

namespace BidCalculationService.test.Applications.Handlers
{
    public class CreateAccountCommandHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CreateAccountCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateAccountCommandHandler(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenUserAlreadyExists()
        {
            var request = Builder<CreateAccountRequest>.CreateNew().Build();
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error!.Description, Is.EqualTo(CreateAccountError.AccountAlreadyExist().Description));
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenAccountCreationFails()
        {
            var request = Builder<CreateAccountRequest>.CreateNew().Build();
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser?)null);
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync((ApplicationUser?)null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error!.Description, Is.EqualTo(CreateAccountError.AccountCreationFailed().Description));
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenAccountIsCreatedSuccessfully()
        {
            var request = Builder<CreateAccountRequest>.CreateNew().Build();

            var user = Builder<ApplicationUser>.CreateNew().Build();
            var responseDto = Builder<CreateAccountResponseDto>.CreateNew().Build();

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser?)null);
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<ApplicationUser>(It.IsAny<CreateAccountRequest>()))
                .Returns(user);
            _mapperMock.Setup(mapper => mapper.Map<CreateAccountResponseDto>(It.IsAny<ApplicationUser>()))
                .Returns(responseDto);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Data, Is.EqualTo(responseDto));
        }
    }
}
