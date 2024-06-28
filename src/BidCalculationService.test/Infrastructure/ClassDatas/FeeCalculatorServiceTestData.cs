using BidCalculationService.Domain.Entities;

namespace BidCalculationService.test.Infrastructure.ClassDatas
{
    public class FeeCalculatorServiceTestData
    {
        public static IEnumerable<object[]> CalculateFee_ShouldReturnCorrectFee_TestData()
        {
            yield return new object[] {
                398.00m,
                VehicleType.COMMON,
                new List<Fee> {
                    new Fee() {
                          FeeType = FeeType.BASIC_FEE,
                          Amount =39.80m,
                    },
                    new Fee() {
                          FeeType = FeeType.SPECIAL_FEE,
                          Amount =7.96m,
                    },
                    new Fee() {
                          FeeType = FeeType.ASSOCIATION_FEE,
                          Amount =5.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.STORAGE_FEE,
                          Amount =100m,
                    }
                },
                550.76m
            };
            yield return new object[] {
                501.00m,
                VehicleType.COMMON,
                new List<Fee> {
                    new Fee() {
                          FeeType = FeeType.BASIC_FEE,
                          Amount =50.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.SPECIAL_FEE,
                          Amount =10.02m,
                    },
                    new Fee() {
                          FeeType = FeeType.ASSOCIATION_FEE,
                          Amount =10.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.STORAGE_FEE,
                          Amount =100m,
                    }
                },
                671.02m
            };
            yield return new object[] {
                57.00m,
                VehicleType.COMMON,
                new List<Fee> {
                    new Fee() {
                          FeeType = FeeType.BASIC_FEE,
                          Amount =10.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.SPECIAL_FEE,
                          Amount =1.14m,
                    },
                    new Fee() {
                          FeeType = FeeType.ASSOCIATION_FEE,
                          Amount =5.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.STORAGE_FEE,
                          Amount =100m,
                    }
                },
                173.14m
            };
            yield return new object[] {
                1800.00m,
                VehicleType.LUXURY,
                new List<Fee> {
                    new Fee() {
                          FeeType = FeeType.BASIC_FEE,
                          Amount =180.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.SPECIAL_FEE,
                          Amount =72.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.ASSOCIATION_FEE,
                          Amount =15.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.STORAGE_FEE,
                          Amount =100m,
                    }
                },
                2167.00m
            };
            yield return new object[] {
                1100.00m,
                VehicleType.COMMON,
                new List<Fee> {
                    new Fee() {
                          FeeType = FeeType.BASIC_FEE,
                          Amount =50.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.SPECIAL_FEE,
                          Amount =22.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.ASSOCIATION_FEE,
                          Amount =15.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.STORAGE_FEE,
                          Amount =100m,
                    }
                },
                1287.00m
            };
            yield return new object[] {
                1000000.00m,
                VehicleType.LUXURY,
                new List<Fee> {
                    new Fee() {
                          FeeType = FeeType.BASIC_FEE,
                          Amount =200.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.SPECIAL_FEE,
                          Amount =40000.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.ASSOCIATION_FEE,
                          Amount =20.00m,
                    },
                    new Fee() {
                          FeeType = FeeType.STORAGE_FEE,
                          Amount =100m,
                    }
                },
                1040320.00m
            };
        }
    }
}
