using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Interfaces.Services;
using BidCalculationService.Infrastruture.Data;
using BidCalculationService.Infrastruture.Data.Repositories;
using BidCalculationService.Infrastruture.Models;
using BidCalculationService.Infrastruture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BidCalculationService.Infrastruture
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!);
            services.AddHttpContextAccessor();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBidRepository, BidRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IFeeCalculatorService, FeeCalculatorService>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            services.AddDbContextPool<AppDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseNpgsql(connectionString);
            });

            using (var context = services.BuildServiceProvider().GetRequiredService<AppDbContext>())
            {
                context.Database.Migrate();
            }

            return services;
        }

    }
}
