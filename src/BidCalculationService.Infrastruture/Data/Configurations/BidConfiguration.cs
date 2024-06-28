using BidCalculationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BidCalculationService.Infrastruture.Data.Configurations
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public BidConfiguration() { }

        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(f => f.VehicleType)
           .IsRequired()
           .HasConversion(
               ft => ft.ToString(),
               str => (VehicleType)Enum.Parse(typeof(VehicleType), str)
           );

            builder.Property(b => b.BasePrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

            builder.Property(b => b.BidBy)
                .IsRequired();

            builder.HasMany(b => b.Fees)
                .WithOne(f => f.Bid)
                .HasForeignKey(f => f.BidId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.User)
                .WithMany(_ => _.Bids)
                .HasForeignKey(b => b.BidBy);
        }
    }
}
