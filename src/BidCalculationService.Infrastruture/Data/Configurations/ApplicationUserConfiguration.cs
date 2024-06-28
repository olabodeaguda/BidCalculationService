using BidCalculationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BidCalculationService.Infrastruture.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("User");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(_ => _.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(_ => _.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(_ => _.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(_ => _.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(_ => _.CreateAt)
                .IsRequired()
                .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

            builder.Property(_ => _.UpdatedBy)
                .HasMaxLength(100);

            builder.HasIndex(_ => _.Email)
                .IsUnique();

        }
    }
}
