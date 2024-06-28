using BidCalculationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BidCalculationService.Infrastruture.Data.Configurations
{
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public FeeConfiguration() { }

        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.FeeType)
           .IsRequired()
           .HasConversion(
               ft => ft.ToString(),
               str => (FeeType)Enum.Parse(typeof(FeeType), str)
           );

            builder.Property(f => f.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(f => f.BidId)
                .IsRequired();

            builder.HasOne(f => f.Bid)
                .WithMany(b => b.Fees)
                .HasForeignKey(f => f.BidId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
