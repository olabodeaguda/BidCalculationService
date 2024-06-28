using BidCalculationService.Domain.Entities;
using BidCalculationService.Infrastruture.Services;
using BidCalculationService.test.Infrastructure.ClassDatas;
using Microsoft.Extensions.Logging;
using Moq;

namespace BidCalculationService.test.Infrastructure
{
    public class ccTest
    {
        private Mock<ILogger<FeeCalculatorService>> _logger;

        [SetUp]
        public void SetUp()
        {

            _logger = new Mock<ILogger<FeeCalculatorService>>();
        }

        [TestCaseSource(typeof(FeeCalculatorServiceTestData), nameof(FeeCalculatorServiceTestData.CalculateFee_ShouldReturnCorrectFee_TestData))]
        public void CalculateFee_ShouldReturnCorrectFee(decimal basePrice, VehicleType vehicleType, List<Fee> fees, decimal total)
        {
            var feeCalculatorService = new FeeCalculatorService(_logger.Object);

            var fee = feeCalculatorService.CalculateTotalFeesAsync(vehicleType, basePrice);

            Assert.IsNotNull(fee);
            Assert.IsTrue(fee.IsSuccess);
            Assert.That(fee.Data!.Sum(_ => _.Amount) + basePrice, Is.EqualTo(total));
            Assert.That(fees.Single(_ => _.FeeType == FeeType.BASIC_FEE).Amount, Is.EqualTo(fee.Data!.Single(_ => _.FeeType == FeeType.BASIC_FEE).Amount));
            Assert.That(fees.Single(_ => _.FeeType == FeeType.SPECIAL_FEE).Amount, Is.EqualTo(fee.Data!.Single(_ => _.FeeType == FeeType.SPECIAL_FEE).Amount));
            Assert.That(fees.Single(_ => _.FeeType == FeeType.ASSOCIATION_FEE).Amount, Is.EqualTo(fee.Data!.Single(_ => _.FeeType == FeeType.ASSOCIATION_FEE).Amount));
            Assert.That(fees.Single(_ => _.FeeType == FeeType.STORAGE_FEE).Amount, Is.EqualTo(fee.Data!.Single(_ => _.FeeType == FeeType.STORAGE_FEE).Amount));
        }
    }
}
