using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace BidCalculationService.Infrastruture.Services
{
    public class FeeCalculatorService : IFeeCalculatorService
    {
        private readonly ILogger<FeeCalculatorService> _logger;
        public FeeCalculatorService(ILogger<FeeCalculatorService> logger)
        {
            _logger = logger;
        }

        public Result<List<Fee>> CalculateTotalFeesAsync(VehicleType vehicleType, decimal basePrice)
        {
            List<Fee> fees = new List<Fee>();

            try
            {
                // basic fee calculation
                decimal basicFee;
                if (vehicleType == VehicleType.COMMON)
                    basicFee = Math.Max(10.0m, Math.Min(0.1m * basePrice, 50.0m));
                else
                    basicFee = Math.Max(25.0m, Math.Min(0.1m * basePrice, 200.0m));
                fees.Add(new Fee { FeeType = FeeType.BASIC_FEE, Amount = basicFee });

                // special fee calculation
                decimal specialFee;
                if (vehicleType == VehicleType.COMMON) specialFee = 0.02m * basePrice;
                else specialFee = 0.04m * basePrice;
                fees.Add(new Fee { FeeType = FeeType.SPECIAL_FEE, Amount = specialFee });

                // association fee calculation
                decimal associationFee;
                if (basePrice <= 500) associationFee = 5.0m;
                else if (basePrice > 500 && basePrice <= 1000) associationFee = 10.0m;
                else if (basePrice > 1000 && basePrice <= 3000) associationFee = 15.0m;
                else associationFee = 20.0m;
                fees.Add(new Fee { FeeType = FeeType.ASSOCIATION_FEE, Amount = associationFee });

                // storage fee
                fees.Add(new Fee { FeeType = FeeType.STORAGE_FEE, Amount = 100.0m });

                return Result<List<Fee>>.Success(fees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calculating fees");

                return Result<List<Fee>>.Failure(BidError.BidCalculationFailed());
            }
        }
    }
}
