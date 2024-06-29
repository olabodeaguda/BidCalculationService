using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Interfaces.Services;
using FizzWare.NBuilder;
using Moq;

namespace BidCalculationService.test.Applications.Handlers
{
    public class CreateBidCommandHandlerTests
    {
        private Mock<IBidRepository> _bidRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IFeeCalculatorService> _feeCalculatorServiceMock;
        private CreateBidCommandHandler _handler;
        private Mock<IClaimService> _claimServiceMock;

        [SetUp]
        public void SetUp()
        {
            _bidRepositoryMock = new Mock<IBidRepository>();
            _mapperMock = new Mock<IMapper>();
            _feeCalculatorServiceMock = new Mock<IFeeCalculatorService>();
            _claimServiceMock = new Mock<IClaimService>();
            _claimServiceMock.Setup(service => service.GetLogOnUserId()).Returns(Guid.NewGuid());
            _handler = new CreateBidCommandHandler(_bidRepositoryMock.Object, _mapperMock.Object, _feeCalculatorServiceMock.Object, _claimServiceMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenFeeCalculationFails()
        {
            var request = Builder<CreateBidRequest>.CreateNew().Build();
            var bid = Builder<Bid>.CreateNew().Build();

            _mapperMock.Setup(mapper => mapper.Map<Bid>(It.IsAny<CreateBidRequest>())).Returns(bid);
            _feeCalculatorServiceMock.Setup(service => service.CalculateTotalFeesAsync(It.IsAny<VehicleType>(), It.IsAny<decimal>()))
                .Returns(Result<List<Fee>>.Failure(BidError.BidCalculationFailed()));

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error!.Description, Is.EqualTo(BidError.FeeCalculationFailed().Description));
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenBidCreationFails()
        {
            var request = Builder<CreateBidRequest>.CreateNew().Build();
            var bid = Builder<Bid>.CreateNew().Build();
            bid.Fees = Builder<Fee>.CreateListOfSize(5).Build().ToList();

            _mapperMock.Setup(mapper => mapper.Map<Bid>(It.IsAny<CreateBidRequest>())).Returns(bid);
            _feeCalculatorServiceMock.Setup(service => service.CalculateTotalFeesAsync(It.IsAny<VehicleType>(), It.IsAny<decimal>()))
                .Returns(Result<List<Fee>>.Success(bid.Fees.ToList()));
            _bidRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Bid>(), It.IsAny<Guid>())).ReturnsAsync((Bid?)null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error!.Description, Is.EqualTo(BidError.BidCalculationFailed().Description));
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenBidIsCreatedSuccessfully()
        {
            var request = Builder<CreateBidRequest>.CreateNew().Build();
            var bid = Builder<Bid>.CreateNew().Build();
            bid.Fees = Builder<Fee>.CreateListOfSize(5).Build().ToList();
            var bidResponseDto = new BidResponseDto();

            _mapperMock.Setup(mapper => mapper.Map<Bid>(request)).Returns(bid);
            _feeCalculatorServiceMock.Setup(service => service.CalculateTotalFeesAsync(It.IsAny<VehicleType>(), It.IsAny<decimal>()))
                .Returns(Result<List<Fee>>.Success(bid.Fees.ToList()));
            _bidRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Bid>(), It.IsAny<Guid>())).ReturnsAsync(bid);
            _mapperMock.Setup(mapper => mapper.Map<BidResponseDto>(It.IsAny<Bid>())).Returns(bidResponseDto);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Data, Is.EqualTo(bidResponseDto));
        }
    }
}
