using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Helpers;

namespace BidCalculationService.Domain.Interfaces.Services
{
    public interface IFeeCalculatorService
    {
        Result<List<Fee>> CalculateTotalFeesAsync(VehicleType vehicleType, decimal basePrice);
    }
}
