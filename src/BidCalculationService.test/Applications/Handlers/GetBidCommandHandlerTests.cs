using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Queries;
using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Interfaces.Repositories;
using FizzWare.NBuilder;
using Moq;

namespace BidCalculationService.test.Applications.Handlers
{
    public class GetBidCommandHandlerTests
    {
        private Mock<IBidRepository> _bidRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private GetBidCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _bidRepositoryMock = new Mock<IBidRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetBidCommandHandler(_bidRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenBidNotFound()
        {
            var request = Builder<GetBidRequest>.CreateNew()
                .With(p => p.Id = 1).Build();
            _bidRepositoryMock.Setup(repo => repo.GetAsync(2)).ReturnsAsync((Bid?)null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error!.Description, Is.EqualTo(BidError.BidFailed().Description));
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenBidIsFound()
        {
            var request = Builder<GetBidRequest>.CreateNew().Build();
            var bid = Builder<Bid>.CreateNew()
                .With(p => p.Id = request.Id)
                .Build();
            var bidResponseDto = Builder<BidResponseDto>.CreateNew()
                .With(p => p.Id = request.Id)
                .Build();

            _bidRepositoryMock.Setup(repo => repo.GetAsync(bid.Id)).ReturnsAsync(bid);
            _mapperMock.Setup(mapper => mapper.Map<BidResponseDto>(It.IsAny<Bid>())).Returns(bidResponseDto);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Data, Is.EqualTo(bidResponseDto));
        }
    }
}
