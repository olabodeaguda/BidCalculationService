using BidCalculationService.Domain.Entities;
using BidCalculationService.Infrastruture.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BidCalculationService.Infrastruture.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Fee> Fees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new BidConfiguration());
            modelBuilder.ApplyConfiguration(new FeeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
