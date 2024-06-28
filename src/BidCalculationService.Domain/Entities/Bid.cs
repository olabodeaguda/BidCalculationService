using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationService.Domain.Entities
{
    public class Bid
    {
        public long Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid BidBy { get; set; }

        public ICollection<Fee> Fees { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public Bid()
        {
            Fees = new HashSet<Fee>();
        }
    }
}
